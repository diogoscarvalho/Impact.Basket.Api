using FluentAssertions;
using Impact.Basket.Api.Mappers;
using Impact.Basket.Api.Models.Requests;

namespace Impact.Basket.Api.UnitTests.Mappers
{
    [TestClass]
    public class ProductMapperTests
    {
        [TestMethod]
        public void ToDomain_ConvertsProductToDomainProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 9.99M,
                Size = 1,
                Stars = 4
            };

            // Act
            var domainProduct = product.ToDomain();

            // Assert
            domainProduct.Id.Should().Be(1);
            domainProduct.Name.Should().Be("Product 1");
            domainProduct.Price.Should().Be(9.99M);
            domainProduct.Size.Should().Be(1);
            domainProduct.Stars.Should().Be(4);
        }
    }

}
