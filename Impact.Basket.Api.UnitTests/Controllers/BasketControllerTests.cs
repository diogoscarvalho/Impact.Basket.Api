using CSharpFunctionalExtensions;
using FluentAssertions;
using Impact.Basket.Api.Controllers;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Requests.Queries;
using Impact.Basket.Api.Models.Responses;
using Impact.Basket.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impact.Basket.Api.UnitTests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        private Mock<IBasketService> basketServiceMock;
        private IUriService uriService = new UriService("https://dummy-uri.com/");
        private BasketController basketController;

        [TestInitialize]
        public void Initialize()
        {
            basketServiceMock = new Mock<IBasketService>();
            basketController = new BasketController(basketServiceMock.Object, uriService);
        }

        [TestMethod]
        public async Task Get_WithPaginationQuery_ReturnsPaginatedBaskets()
        {
            // Arrange
            var paginationQuery = new PaginationQuery { PageNumber = 1, PageSize = 10 };
            var paginationFilter = new PaginationFilter(1, 10);
            var baskets = new List<Api.Domain.Models.Basket> { new Api.Domain.Models.Basket(), new Api.Domain.Models.Basket(), new Api.Domain.Models.Basket(), new Api.Domain.Models.Basket(), new Api.Domain.Models.Basket(), new Api.Domain.Models.Basket() };
            var result = Result.Success(baskets.AsEnumerable());

            basketServiceMock.Setup(mock => mock.GetAll(It.IsAny<PaginationFilter>())).ReturnsAsync(result);

            // Act
            var response = await basketController.Get(paginationQuery);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [TestMethod]
        public async Task Get_WithInvalidPaginationQuery_ReturnsNotFound()
        {
            // Arrange
            var paginationQuery = new PaginationQuery { PageNumber = 0, PageSize = 0 };
            var result = Result.Failure<IEnumerable<Api.Domain.Models.Basket>>("Invalid pagination query");

            basketServiceMock.Setup(mock => mock.GetAll(It.IsAny<PaginationFilter>())).Returns(Task.FromResult(result));

            // Act
            var response = await basketController.Get(paginationQuery);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = response as NotFoundObjectResult;
            notFoundResult.Value.Should().Be(result.Error);
        }

        [TestMethod]
        public async Task GetById_ExistingId_ReturnsBasket()
        {
            // Arrange
            var basketId = Guid.NewGuid();
            var basket = new Api.Domain.Models.Basket();
            var result = Result.Success(basket);

            basketServiceMock.Setup(mock => mock.GetById(basketId)).ReturnsAsync(result);

            // Act
            var response = await basketController.Get(basketId);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            var okResult = response as OkObjectResult;
            okResult.Value.Should().BeOfType<Response<Api.Domain.Models.Basket>>();
            var basketResponse = okResult.Value as Response<Api.Domain.Models.Basket>;
            basketResponse.Data.Should().Be(basket);
        }

        [TestMethod]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var basketId = Guid.NewGuid();
            var result = Result.Failure<Api.Domain.Models.Basket>("Basket not found");

            basketServiceMock.Setup(mock => mock.GetById(basketId)).Returns(Task.FromResult(result));

            // Act
            var response = await basketController.Get(basketId);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = response as NotFoundObjectResult;
            notFoundResult.Value.Should().Be(result.Error);
        }
    }
}
