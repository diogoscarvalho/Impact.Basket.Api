using FluentAssertions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace Impact.Basket.Api.UnitTests.Repositories
{
    [TestClass]
    public class InMemoryProductRepositoryTests
    {
        private Mock<ILogger<InMemoryProductRepository>> _loggerMock;
        private InMemoryProductRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger<InMemoryProductRepository>>();
            _repository = new InMemoryProductRepository(_loggerMock.Object);
        }

        [TestMethod]
        public async Task Create_NewProduct_AddsProductToCollection()
        {
            // Arrange
            var product = new Product(1, "Product 1", 12.00M, 2, 3);

            // Act
            var result = await _repository.Create(product);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public async Task Create_ExistingProduct_ReturnsFailureResult()
        {
            // Arrange
            var existingProduct = new Product(1, "Product 1", 12.00M, 2, 3);
            await _repository.Create(existingProduct);

            var product = new Product(1, "Product 1", 12.00M, 2, 3);

            // Act
            var result = await _repository.Create(product);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be($"Cannot insert product with identity '{product.Id}' as it already exists in the collection");
        }

        [TestMethod]
        public async Task Delete_ExistingProduct_RemovesProductFromCollection()
        {
            // Arrange
            var product = new Product(1, "Product 1", 12.00M, 2, 3);
            await _repository.Create(product);

            // Act
            var result = await _repository.Delete(product.Id);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public async Task Delete_NonExistingProduct_ReturnsFailureResult()
        {
            // Arrange
            var productId = 1;

            // Act
            var result = await _repository.Delete(productId);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be($"Cannot delete product with identity '{productId}' as it doesn't exist");
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllProducts()
        {
            // Arrange
            var product1 = new Product(12, "Product 12", 25.00M, 2, 3);
            var product2 = new Product(15, "Product 15", 12.00M, 2, 4);

            await _repository.Create(product1);
            await _repository.Create(product2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Count().Should().Be(2);
            result.Value.Any(x => x.Id.Equals(product1.Id)).Should().BeTrue();
            result.Value.Any(x => x.Id.Equals(product2.Id)).Should().BeTrue();
        }

        [TestMethod]
        public async Task GetById_ExistingProduct_ReturnsProduct()
        {
            // Arrange
            var product = new Product(12, "Product 12", 25.00M, 2, 3);
            await _repository.Create(product);

            // Act
            var result = await _repository.GetById(product.Id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Id.Should().Be(product.Id);
        }

        [TestMethod]
        public async Task GetById_NonExistingProduct_ReturnsFailureResult()
        {
            // Arrange
            var productId = 1;

            // Act
            var result = await _repository.GetById(productId);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public async Task Update_ExistingProduct_UpdatesProduct()
        {
            // Arrange
            var product = new Product(12, "Product 12", 25.00M, 2, 3);
            await _repository.Create(product);

            var updatedProduct = new Product(12, "Product 12", 25.00M, 2, 5);

            // Act
            var result = await _repository.Update(updatedProduct);
            var getByIdResult = await _repository.GetById(product.Id);

            var newProductVersion = getByIdResult.Value;

            // Assert
            Assert.IsTrue(result.IsSuccess);

            newProductVersion.Id.Should().Be(updatedProduct.Id);
            newProductVersion.Name.Should().Be(updatedProduct.Name);
            newProductVersion.Price.Should().Be(updatedProduct.Price);
            newProductVersion.Stars.Should().Be(updatedProduct.Stars);
        }

        [TestMethod]
        public async Task Update_NonExistingProduct_ReturnsFailureResult()
        {
            // Arrange
            var product = new Product(1, "Product 4", 12.00M, 4, 3);

            // Act
            var result = await _repository.Update(product);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNullOrWhiteSpace().And.ContainAll($"Cannot update product with identity '{product.Id}' as it doesn't exist");
        }

        [TestMethod]
        public async Task GetAllPaged_ReturnsPagedProducts()
        {
            // Arrange
            var product1 = new Product(1, "Product 1", 12.00M, 3, 3);
            var product2 = new Product(16, "Product 16", 12.00M, 4, 3);
            var product3 = new Product(22, "Product 22", 12.00M, 2, 3);
            var product4 = new Product(25, "Product 25", 12.00M, 5, 3);

            await _repository.Create(product1);
            await _repository.Create(product2);
            await _repository.Create(product3);
            await _repository.Create(product4);

            var pageNumber = 2;
            var pageSize = 2;

            // Act
            var result = await _repository.GetAll(new PaginationFilter(pageNumber, pageSize));

            // Assert
            result.IsSuccess.Should().BeTrue();
            pageSize.Should().Be(result.Value.Count());
        }
    }
}
