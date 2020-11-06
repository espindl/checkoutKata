using System.Collections.Generic;
using Xunit;

namespace Checkout.Tests
{
    public class BasketTests
    {
        private readonly Dictionary<string, decimal> _prices;

        public BasketTests()
        {
            _prices = new Dictionary<string, decimal>
            {
                {"A99", 0.5M},
                {"B15", 0.3M},
                {"C40", 0.6M}
            };
        }
        
        [Fact]
        public void ReturnsZeroIfNoItemsScanned()
        {
            var basket = new Basket(_prices);
            Assert.Equal(0.0M,basket.GetTotal());
        }
        
        [Fact]
        public void ReturnsTotalForTwoItems()
        {
            var basket = new Basket(_prices);
            basket.Scan("B15");
            basket.Scan("A99");
            Assert.Equal(0.8M,basket.GetTotal());
        }
    }
}