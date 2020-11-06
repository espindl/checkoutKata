using System.Collections.Generic;
using Xunit;

namespace Checkout.Tests
{
    public class BasketTests
    {
        private readonly Dictionary<string, decimal> _prices;
        private readonly List<SpecialOffer> _offers;

        public BasketTests()
        {
            _prices = new Dictionary<string, decimal>
            {
                {"A99", 0.5M},
                {"B15", 0.3M},
                {"C40", 0.6M}
            };
            
            _offers = new List<SpecialOffer>
            {
                new SpecialOffer("A99", 3, 1.3M),
                new SpecialOffer("B15", 2, 0.45M),
            };
        }
        
        [Fact]
        public void ReturnsZeroIfNoItemsScanned()
        {
            var basket = new Basket(_prices, _offers);
            Assert.Equal(0.0M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsTotalForTwoItems()
        {
            var basket = new Basket(_prices, _offers);
            basket.Scan("B15");
            basket.Scan("A99");
            Assert.Equal(0.8M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsRightTotalForItemListContainingSpecialOffer()
        {
            var basket = new Basket(_prices,_offers);
            basket.Scan("B15");
            basket.Scan("A99");
            basket.Scan("B15");
            Assert.Equal(0.95M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsRightTotalForItemListNotContainingAnySpecialOffer()
        {
            var basket = new Basket(_prices,_offers);
            basket.Scan("C40");
            basket.Scan("C40");
            Assert.Equal(1.2M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsRightTotalForItemListContainingSpecialOfferAndMore()
        {
            var basket = new Basket(_prices,_offers);
            basket.Scan("B15");
            basket.Scan("B15");
            basket.Scan("A99");
            basket.Scan("B15");
            Assert.Equal(1.25M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsRightTotalForItemListContainingSpecialOfferMultipleTimes()
        {
            var basket = new Basket(_prices,_offers);
            basket.Scan("B15");
            basket.Scan("B15");
            basket.Scan("A99");
            basket.Scan("B15");
            basket.Scan("B15");
            Assert.Equal(1.4M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsRightTotalForItemListContainingSpecialOfferMultipleTimesAndMore()
        {
            var basket = new Basket(_prices,_offers);
            basket.Scan("B15");
            basket.Scan("B15");
            basket.Scan("A99");
            basket.Scan("B15");
            basket.Scan("B15");
            basket.Scan("B15");
            Assert.Equal(1.7M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsRightTotalForItemListContainingOnlySpecialOffer()
        {
            var basket = new Basket(_prices,_offers);
            basket.Scan("A99");
            basket.Scan("A99");
            basket.Scan("A99");
            Assert.Equal(1.3M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsRightTotalForItemListContainingOnlySpecialOffers()
        {
            var basket = new Basket(_prices,_offers);
            basket.Scan("A99");
            basket.Scan("A99");
            basket.Scan("A99");
            basket.Scan("B15");
            basket.Scan("B15");
            Assert.Equal(1.75M,basket.GetTotal());
        }
    }
}