using FluentAssertions;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Requests;
using Impact.Basket.Api.Models.Responses;
using Impact.Basket.Api.Repository;
using Impact.Basket.Api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Impact.Basket.Api.IntegrationTests.Services
{
    [TestClass]
    public class BasketServiceIntegrationTests
    {
        private Mock<ILogger<BasketService>> _loggerMock;
        private IGenericRepository<Api.Domain.Models.Basket, Guid> _basketRepository;
        private Mock<ICodeChallengeApiService<OrderRequest, OrderResponse>> _codeChallengeApiServiceMock;
        private BasketService _basketService;
        private Mock<ILogger<InMemoryBasketRepository>> _repoLoggerMock;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger<BasketService>>();
            _repoLoggerMock = new Mock<ILogger<InMemoryBasketRepository>>();

            _basketRepository = new InMemoryBasketRepository(_repoLoggerMock.Object);
            _codeChallengeApiServiceMock = new Mock<ICodeChallengeApiService<OrderRequest, OrderResponse>>();

            _basketService = new BasketService(
                _loggerMock.Object,
                _basketRepository,
                _codeChallengeApiServiceMock.Object);
        }

        [TestMethod]
        public async Task Create_WithValidBasket_ReturnsSuccessResultWithBasketId()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            var basketId = basket.Id;

            // Act
            var result = await _basketService.Create(basket);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(basketId);
        }

        [TestMethod]
        public async Task Delete_WithExistingBasketId_ReturnsSuccessResult()
        {
            // Arrange
            var basket = new Domain.Models.Basket();
            var basketId = basket.Id;

            await _basketRepository.Create(basket);

            // Act
            var result = await _basketService.Delete(basketId);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public async Task GetById_WithExistingBasketId_ReturnsSuccessResultWithBasket()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            var basketId = basket.Id;

            await _basketRepository.Create(basket);

            // Act
            var result = await _basketService.GetById(basketId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Id.Should().Be(basket.Id);
        }

        [TestMethod]
        public async Task Update_WithValidBasket_ReturnsSuccessResult()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            await _basketRepository.Create(basket);

            basket.AddItem(new Domain.Models.BasketItem(new Domain.Models.Product(12, "Product 12", 29.00M, 2, 5), 3));

            // Act
            var result = await _basketService.Update(basket);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
