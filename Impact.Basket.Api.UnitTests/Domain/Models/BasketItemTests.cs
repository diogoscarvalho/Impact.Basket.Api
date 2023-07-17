using FluentAssertions;
using Impact.Basket.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impact.Basket.Api.UnitTests.Domain.Models
{
    [TestClass]
    public class BasketItemTests
    {
        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            // Arrange
            var product = new Product(1, "Product 1", 12.00M, 2, 3);
            var quantity = 2;

            // Act
            var basketItem = new BasketItem(product, quantity);

            // Assert
            basketItem.Product.Should().Be(product);
            basketItem.Quantity.Should().Be(quantity);
            basketItem.TotalPrice.Should().Be(product.Price * quantity);
        }

        [TestMethod]
        public void IncreaseQuantity_IncrementsQuantityAndAdjustsTotalPrice()
        {
            // Arrange
            var product = new Product(1, "Product 1", 12.00M, 2, 3);
            var quantity = 2;
            var basketItem = new BasketItem(product, quantity);

            // Act
            basketItem.IncreaseQuantity(1);

            // Assert
            basketItem.Quantity.Should().Be(quantity + 1);
            basketItem.TotalPrice.Should().Be(product.Price * (quantity + 1));
        }

        [TestMethod]
        public void DecreaseQuantity_DecrementsQuantityAndAdjustsTotalPrice()
        {
            // Arrange
            var product = new Product(1, "Product 1", 12.00M, 2, 3);
            var quantity = 2;
            var basketItem = new BasketItem(product, quantity);

            // Act
            basketItem.DecreaseQuantity();

            // Assert
            basketItem.Quantity.Should().Be(quantity - 1);
            basketItem.TotalPrice.Should().Be(product.Price * (quantity - 1));
        }

        [TestMethod]
        public void DecreaseQuantity_WithZeroQuantity_DoesNotChangeQuantityOrTotalPrice()
        {
            // Arrange
            var product = new Product(1, "Product 1", 12.00M, 2, 3);
            var quantity = 0;
            var basketItem = new BasketItem(product, quantity);

            // Act
            basketItem.DecreaseQuantity();

            // Assert
            basketItem.Quantity.Should().Be(quantity);
            basketItem.TotalPrice.Should().Be(product.Price * quantity);
        }
    }

}
