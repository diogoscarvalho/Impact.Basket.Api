using FluentAssertions;
using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Concurrent;

namespace Impact.Basket.Api.UnitTests.Repositories
{
    [TestClass]
    public class InMemoryBasketRepositoryTests
    {
        private Mock<ILogger<InMemoryBasketRepository>> _loggerMock;
        private InMemoryBasketRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger<InMemoryBasketRepository>>();
            _repository = new InMemoryBasketRepository(_loggerMock.Object);
        }

        [TestMethod]
        public async Task Create_NewBasket_AddsBasketToCollection()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                     new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                     {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                     }),
                                     BasketStatus.Open);

            // Act
            var result = await _repository.Create(basket);

            // Assert
            result.IsSuccess.Should().BeTrue();
            var basketResult = await _repository.GetById(basket.Id);

            basketResult.IsSuccess.Should().BeTrue();
            basketResult.Value.Id.Should().Be(basket.Id);
        }

        [TestMethod]
        public async Task Create_ExistingBasket_ReturnsFailureResult()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                     new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                     {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                     }),
                                     BasketStatus.Open);

            await _repository.Create(basket);

            // Act
            var result = await _repository.Create(basket);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be($"Cannot insert basket with identity '{basket.Id}' as it already exists in the collection");
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllBaskets()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                     new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                     {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                     }),
                                     BasketStatus.Open);

            var basket2 = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                    new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                    {
                                         new KeyValuePair<int, BasketItem>(1, new Api.Domain.Models.BasketItem(new Product(112, "Product 1", 25.00M, 1, 2), 1)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 35.00M, 4, 5), 1))
                                    }),
                                    BasketStatus.Open);

            await _repository.Create(basket);
            await _repository.Create(basket2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Count().Should().Be(2);
            result.Value.Any(x => x.Id.Equals(basket.Id)).Should().BeTrue();
            result.Value.Any(x => x.Id.Equals(basket2.Id)).Should().BeTrue();
        }

        [TestMethod]
        public async Task GetById_ExistingBasket_ReturnsBasket()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                       new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                       {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                       }),
                                       BasketStatus.Open);

            await _repository.Create(basket);

            // Act
            var result = await _repository.GetById(basket.Id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Id.Should().Be(basket.Id);
        }

        [TestMethod]
        public async Task GetById_NonExistingBasket_ReturnsFailureResult()
        {
            // Arrange
            var basketId = Guid.NewGuid();

            // Act
            var result = await _repository.GetById(basketId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be($"Basket with identity {basketId} not found!");
        }

        [TestMethod]
        public async Task Update_ExistingBasket_UpdatesBasket()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                      new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                      {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                      }),
                                      BasketStatus.Open);

            await _repository.Create(basket);

            var updatedBasket = new Api.Domain.Models.Basket(basket.Id,
                                     new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                     {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 4))
                                     }),
                                     BasketStatus.Open);

            // Act
            var result = await _repository.Update(updatedBasket);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public async Task Update_NonExistingBasket_ReturnsFailureResult()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                     new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                     {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                     }),
                                     BasketStatus.Open);

            // Act
            var result = await _repository.Update(basket);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be($"Cannot update basket with identity '{basket.Id}' as it doesn't exist");
        }

        [TestMethod]
        public async Task Delete_ExistingBasket_RemovesBasketFromCollection()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                      new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                      {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                      }),
                                      BasketStatus.Open);

            await _repository.Create(basket);

            // Act
            var result = await _repository.Delete(basket.Id);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public async Task Delete_NonExistingBasket_ReturnsFailureResult()
        {
            // Arrange
            var basketId = Guid.NewGuid();

            // Act
            var result = await _repository.Delete(basketId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be($"Cannot delete basket with identity '{basketId}' as it doesn't exist");
        }

        [TestMethod]
        public async Task GetByFilter_BasketMatchesPredicate_ReturnsMatchingBaskets()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                     new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                     {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                     }),
                                     BasketStatus.Open);

            var basket2 = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                    new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                    {
                                         new KeyValuePair<int, BasketItem>(1, new Api.Domain.Models.BasketItem(new Product(112, "Product 1", 25.00M, 1, 2), 1)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 35.00M, 4, 5), 1))
                                    }),
                                    BasketStatus.Open);

            var basket3 = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                    new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                    {
                                         new KeyValuePair<int, BasketItem>(1, new Api.Domain.Models.BasketItem(new Product(112, "Product 1", 25.00M, 1, 2), 1)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 35.00M, 4, 5), 1))
                                    }),
                                    BasketStatus.Ordered);

            await _repository.Create(basket);
            await _repository.Create(basket2);
            await _repository.Create(basket3);

            Func<Api.Domain.Models.Basket, bool> predicate = basket => basket.BasketStatus.Equals(BasketStatus.Open);

            // Act
            var result = await _repository.GetByFilter(predicate);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task GetByFilter_NoMatchingBaskets_ReturnsFailureResult()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                     new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                     {
                                         new KeyValuePair<int, BasketItem>(112, new Api.Domain.Models.BasketItem(new Product(112, "Product 112", 75.00M, 1, 2), 2)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 65.00M, 4, 4), 2))
                                     }),
                                     BasketStatus.Open);

            var basket2 = new Api.Domain.Models.Basket(Guid.NewGuid(),
                                    new ConcurrentDictionary<int, BasketItem>(new List<KeyValuePair<int, BasketItem>>
                                    {
                                         new KeyValuePair<int, BasketItem>(1, new Api.Domain.Models.BasketItem(new Product(112, "Product 1", 25.00M, 1, 2), 1)),
                                         new KeyValuePair<int, BasketItem>(4, new Api.Domain.Models.BasketItem(new Product(4, "Product 4", 35.00M, 4, 5), 1))
                                    }),
                                    BasketStatus.Open);

            await _repository.Create(basket);
            await _repository.Create(basket2);

            Func<Api.Domain.Models.Basket, bool> predicate = basket => basket.Id.Equals(Guid.NewGuid());

            // Act
            var result = await _repository.GetByFilter(predicate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("There are no baskets to the given predicate");
        }
    }
}
