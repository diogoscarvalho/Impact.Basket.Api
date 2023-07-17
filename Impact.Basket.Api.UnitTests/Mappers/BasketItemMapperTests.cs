using FluentAssertions;
using Impact.Basket.Api.Mappers;
using Impact.Basket.Api.Models.Requests;

namespace Impact.Basket.Api.UnitTests.Mappers
{
    [TestClass]
    public class BasketItemMapperTests
    {
        [TestMethod]
        public void ToDomain_ConvertsBasketItemsToDomainBasketItems()
        {
            // Arrange
            var basketItems = new List<BasketItem>
            {
                new BasketItem
                {
                    Product = new Product { Id = 1, Name = "Product 1", Price = 10.00M, Size = 1, Stars = 3 },
                    Quantity = 2
                },
                new BasketItem
                {
                    Product = new Product { Id = 2, Name = "Product 2", Price = 15.00M, Size = 2, Stars = 4 },
                    Quantity = 3
                }
            };

            // Act
            var domainBasketItems = basketItems.ToDomain();

            // Assert
            domainBasketItems.Should().HaveCount(2);
            domainBasketItems.Should().Contain(basketItem =>
                basketItem.Product.Id == 1 && basketItem.Product.Name == "Product 1" && basketItem.Quantity == 2);
            domainBasketItems.Should().Contain(basketItem =>
                basketItem.Product.Id == 2 && basketItem.Product.Name == "Product 2" && basketItem.Quantity == 3);
        }

        [TestMethod]
        public void ToDomain_ConvertsBasketItemToDomainBasketItem()
        {
            // Arrange
            var basketItem = new BasketItem
            {
                Product = new Product { Id = 1, Name = "Product 1", Price = 10.00M, Size = 1 },
                Quantity = 2
            };

            // Act
            var domainBasketItem = basketItem.ToDomain();

            // Assert
            domainBasketItem.Should().NotBeNull();
            domainBasketItem.Product.Id.Should().Be(1);
            domainBasketItem.Product.Name.Should().Be("Product 1");
            domainBasketItem.Quantity.Should().Be(2);
        }

        [TestMethod]
        public void ToOrderLine_ConvertsBasketItemToOrderLine()
        {
            // Arrange
            var basketItem = new Api.Domain.Models.BasketItem(new Api.Domain.Models.Product(1, "Product 1", 10.00M, 1, 2), 2);

            // Act
            var orderLine = basketItem.ToOrderLine();

            // Assert
            orderLine.Should().NotBeNull();
            orderLine.ProductId.Should().Be(1);
            orderLine.ProductName.Should().Be("Product 1");
            orderLine.ProductSize.Should().Be("1");
            orderLine.ProductUnitPrice.Should().Be(10.00M);
            orderLine.Quantity.Should().Be(2);
            orderLine.TotalPrice.Should().Be(20.00M);
        }
    }
}
