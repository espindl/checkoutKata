using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class Basket
    {
        private readonly Dictionary<string, decimal> _prices;
        private readonly Dictionary<string, int> _items;

        public Basket(Dictionary<string, decimal> prices)
        {
            _prices = prices;
            _items = new Dictionary<string, int>();
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
        
        public decimal GetTotal()
        {
            decimal total = 0;
            total += _items.Sum(item => _prices[item.Key] * item.Value);
            return total;
        }
    }
}