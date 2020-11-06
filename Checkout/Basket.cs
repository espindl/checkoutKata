using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class Basket
    {
        private readonly Dictionary<string, decimal> _prices;
        private readonly Dictionary<string, int> _items;
        private readonly List<SpecialOffer> _offers;

        public Basket(Dictionary<string, decimal> prices, List<SpecialOffer> offers)
        {
            _prices = prices;
            _items = new Dictionary<string, int>();
            _offers = offers;
        }
        
        public void Scan(string sku)
        {
            if (_items.ContainsKey(sku))
            {
                _items[sku] += 1;
                return;
            }
            _items.Add(sku,1);
        }
        
        public decimal GetTotal(bool applySpecialOffers = true)
        {
            decimal total = 0;
            if (_items.Count == 0) return total;
            var tempItems = new Dictionary<string, int>(_items);

            if (applySpecialOffers)
            {
                foreach (var offer in _offers)
                {
                    var itemCount = tempItems.ContainsKey(offer.Sku) ? tempItems[offer.Sku] : 0;
                    if (itemCount < offer.Quantity) continue;
                    var countSubjectToOffer = itemCount / offer.Quantity;
                    total += offer.OfferPrice * countSubjectToOffer;
                    tempItems[offer.Sku] -= (countSubjectToOffer * offer.Quantity);
                }
            }

            total += tempItems.Sum(item => _prices[item.Key] * item.Value);
            return total;
        }
    }
}