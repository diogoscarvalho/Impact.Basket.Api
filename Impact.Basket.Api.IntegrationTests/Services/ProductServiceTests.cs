using CSharpFunctionalExtensions;
using FluentAssertions;
using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Repository;
using Impact.Basket.Api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impact.Basket.Api.IntegrationTests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<ILogger<ProductService>> _logger;
        private Mock<ILogger<InMemoryProductRepository>> _repoLogger;
        private IGenericRepository<Domain.Models.Product, int> _repository;
        private ProductService _productService;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger<ProductService>>();
            _repoLogger = new Mock<ILogger<InMemoryProductRepository>>();
            _repository = new InMemoryProductRepository(_repoLogger.Object);
            _productService = new ProductService(_repository, _logger.Object);
        }

        [TestMethod]
        public async Task Create_WithValidProduct_ReturnsSuccessResultWithProductId()
        {
            // Arrange
            var product = new Api.Domain.Models.Product(1, "Product 1", 10.00M, 1, 5);

            // Act
            var result = await _productService.Create(product);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(product.Id);
        }

        [TestMethod]
        public async Task Delete_WithExistingProductId_ReturnsSuccessResult()
        {
            // Arrange
            var product = new Api.Domain.Models.Product(1, "Product 1", 10.00M, 1, 5);
            await _repository.Create(product);

            var productId = 1;

            // Act
            var result = await _productService.Delete(productId);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public async Task GetCheapest_WithValidNumberOfProducts_ReturnsSuccessResultWithCheapestProducts()
        {
            // Arrange
            var numberOfProducts = 3;
            List<Product> products = new()
            {
                new Domain.Models.Product(1, "Product 1", 10.00M, 1, 5),
                new Domain.Models.Product(2, "Product 2", 5.00M, 2, 3),
                new Domain.Models.Product (3, "Product 3", 8.00M, 3, 4),
                new Domain.Models.Product(4, "Product 4", 2.50M, 4, 2),
            };
            products.ForEach(async product => await _repository.Create(product));

            // Act
            var result = await _productService.GetCheapest(numberOfProducts);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(numberOfProducts);
            result.Value.Should().BeInAscendingOrder(p => p.Price);
        }

        [TestMethod]
        public async Task GetTopRanked_WithValidNumberOfProducts_ReturnsSuccessResultWithTopRankedProducts()
        {
            // Arrange
            var numberOfProducts = 2;
            List<Product> products = new()
            {
                new Domain.Models.Product(1, "Product 1", 10.00M, 1, 5),
                new Domain.Models.Product(2, "Product 2", 5.00M, 2, 4),
                new Domain.Models.Product (3, "Product 3", 8.00M, 3, 3),
                new Domain.Models.Product(4, "Product 4", 2.50M, 4, 2),
            };
            products.ForEach(async product => await _repository.Create(product));

            // Act
            var result = await _productService.GetTopRanked(numberOfProducts);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(numberOfProducts);
            result.Value.Should().BeInDescendingOrder(p => p.Stars);
        }
    }
}
