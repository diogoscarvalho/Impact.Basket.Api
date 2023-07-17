using FluentAssertions;
using Impact.Basket.Api.Mappers;
using Impact.Basket.Api.Models.Requests;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Impact.Basket.Api.UnitTests.Mappers
{
    [TestClass]
    public class BasketMapperTests
    {
        [TestMethod]
        public void ToDomain_ConvertsBasketRequestToDomainBasket()
        {
            // Arrange
            var basketRequest = new BasketRequest
            {
                Items = new List<BasketItem>
                {
                    new BasketItem { Product = new Product{ Id = 1, Name = "Product 1", Price = 10.00M, Size = 1, Stars = 3 }, Quantity = 2 },
                    new BasketItem { Product = new Product{ Id = 2, Name = "Product 2", Price = 25.00M, Size = 1, Stars = 4 }, Quantity = 3 }
                }
            };

            // Act
            var domainBasket = basketRequest.ToDomain();

            // Assert
            domainBasket.Should().NotBeNull();
            domainBasket.Items.Should().HaveCount(2);
            domainBasket.Items.Should().ContainKey(1);
            domainBasket.Items.Should().ContainKey(2);
            domainBasket.Items[1].Quantity.Should().Be(2);
            domainBasket.Items[2].Quantity.Should().Be(3);
        }

        [TestMethod]
        public void ToOrder_ConvertsDomainBasketToOrderRequest()
        {
            // Arrange
            var domainBasket = new Api.Domain.Models.Basket();
            domainBasket.AddItem(new Api.Domain.Models.BasketItem(new Api.Domain.Models.Product(1, "Product 1", 10.00M, 2, 4), 2));
            domainBasket.AddItem(new Api.Domain.Models.BasketItem(new Api.Domain.Models.Product(2, "Product 2", 15.00M, 2, 4), 3));

            // Act
            var orderRequest = domainBasket.ToOrder();

            // Assert
            orderRequest.Should().NotBeNull();
            orderRequest.UserEmail.Should().Be("diogo.carvalho@email.com");
            orderRequest.OrderLines.Should().HaveCount(2);
            orderRequest.OrderLines.Should().ContainSingle(orderLine =>
                orderLine.ProductId == 1 &&
                orderLine.ProductName == "Product 1" &&
                orderLine.ProductSize == "2" &&
                orderLine.ProductUnitPrice == 10.00M &&
                orderLine.Quantity == 2 &&
                orderLine.TotalPrice == 20.00M);
            orderRequest.OrderLines.Should().ContainSingle(orderLine =>
                orderLine.ProductId == 2 &&
                orderLine.ProductName == "Product 2" &&
                orderLine.ProductSize == "2" &&
                orderLine.ProductUnitPrice == 15.00M &&
                orderLine.Quantity == 3 &&
                orderLine.TotalPrice == 45.00M);
            orderRequest.TotalAmount.Should().Be(65.00M);
        }
    }

}
