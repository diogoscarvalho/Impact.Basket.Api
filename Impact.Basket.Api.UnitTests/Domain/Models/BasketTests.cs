using FluentAssertions;
using Impact.Basket.Api.Domain.Models;

namespace Impact.Basket.Api.UnitTests.Domain.Models
{
    [TestClass]
    public class BasketTests
    {
        [TestMethod]
        public void AddItem_NewItem_AddsItemToBasket()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            var item = new BasketItem(new Product(1, "Product 1", 45.00M, 3, 5), 1);

            // Act
            basket.AddItem(item);

            // Assert
            basket.Items.Should().ContainKey(item.Product.Id);
            basket.Items[item.Product.Id].Should().Be(item);
        }

        [TestMethod]
        public void AddItem_ExistingItem_IncreasesItemQuantity()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            var product = new Product(1, "Product 1", 45.00M, 3, 5);
            var item1 = new BasketItem(product, 1);
            var item2 = new BasketItem(product, 1);

            // Act
            basket.AddItem(item1);
            basket.AddItem(item2);

            // Assert
            basket.Items[product.Id].Quantity.Should().Be(2);
        }

        [TestMethod]
        public void RemoveItem_ExistingItemWithQuantityOne_RemovesItemFromBasket()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            var item = new BasketItem(new Product(1, "Product 1", 45.00M, 3, 5), 1);
            basket.AddItem(item);

            // Act
            basket.RemoveItem(item);

            // Assert
            basket.Items.Should().NotContainKey(item.Product.Id);
        }

        [TestMethod]
        public void RemoveItem_ExistingItemWithQuantityGreaterThanOne_DecreasesItemQuantity()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            var product = new Product(1, "Product 1", 45.00M, 3, 5);
            var item1 = new BasketItem(product, 1);
            var item2 = new BasketItem(product, 2);
            basket.AddItem(item1);
            basket.AddItem(item2);

            // Act
            basket.RemoveItem(item1);

            // Assert
            basket.Items[product.Id].Quantity.Should().Be(2);
        }
    }

}
