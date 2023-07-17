using CSharpFunctionalExtensions;
using FluentAssertions;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Requests;
using Impact.Basket.Api.Models.Responses;
using Impact.Basket.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Impact.Basket.Api.UnitTests.Services
{
    [TestClass]
    public class BasketServiceTests
    {
        private Mock<ILogger<BasketService>> _loggerMock;
        private Mock<IGenericRepository<Api.Domain.Models.Basket, Guid>> _basketRepositoryMock;
        private Mock<ICodeChallengeApiService<OrderRequest, OrderResponse>> _codeChallengeApiServiceMock;
        private BasketService _basketService;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger<BasketService>>();
            _basketRepositoryMock = new Mock<IGenericRepository<Api.Domain.Models.Basket, Guid>>();
            _codeChallengeApiServiceMock = new Mock<ICodeChallengeApiService<OrderRequest, OrderResponse>>();

            _basketService = new BasketService(
                _loggerMock.Object,
                _basketRepositoryMock.Object,
                _codeChallengeApiServiceMock.Object);
        }

        [TestMethod]
        public async Task Create_WithValidBasket_ReturnsSuccessResultWithBasketId()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            var basketId = Guid.NewGuid();
            _basketRepositoryMock.Setup(x => x.Create(basket)).ReturnsAsync(Result.Success(basketId));

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
            var basketId = Guid.NewGuid();
            _basketRepositoryMock.Setup(x => x.Delete(basketId)).ReturnsAsync(Result.Success());

            // Act
            var result = await _basketService.Delete(basketId);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public async Task GetById_WithExistingBasketId_ReturnsSuccessResultWithBasket()
        {
            // Arrange
            var basketId = Guid.NewGuid();
            var basket = new Api.Domain.Models.Basket();
            _basketRepositoryMock.Setup(x => x.GetById(basketId)).ReturnsAsync(Result.Success(basket));

            // Act
            var result = await _basketService.GetById(basketId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(basket);
        }

        [TestMethod]
        public async Task Update_WithValidBasket_ReturnsSuccessResult()
        {
            // Arrange
            var basket = new Api.Domain.Models.Basket();
            _basketRepositoryMock.Setup(x => x.Update(basket)).ReturnsAsync(Result.Success());

            // Act
            var result = await _basketService.Update(basket);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
