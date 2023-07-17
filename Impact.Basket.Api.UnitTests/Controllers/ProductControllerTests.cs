using CSharpFunctionalExtensions;
using FluentAssertions;
using Impact.Basket.Api.Controllers;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Requests.Queries;
using Impact.Basket.Api.Models.Responses;
using Impact.Basket.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impact.Basket.Api.UnitTests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        private Mock<ILogger<ProductController>> loggerMock;
        private IUriService uriService = new UriService("http://example.com/");
        private Mock<IProductService> productServiceMock;
        private ProductController productController;

        [TestInitialize]
        public void Initialize()
        {
            loggerMock = new Mock<ILogger<ProductController>>();
            productServiceMock = new Mock<IProductService>();
            productController = new ProductController(loggerMock.Object, uriService, productServiceMock.Object);
        }

        [TestMethod]
        public async Task Get_ValidPaginationQuery_ReturnsOkResultWithPaginatedResponse()
        {
            // Arrange
            var paginationQuery = new PaginationQuery { PageNumber = 1, PageSize = 10 };
            var paginationFilter = new PaginationFilter(1, 10);
            var products = new List<Api.Domain.Models.Product> { new Api.Domain.Models.Product(1, "Product 1", 12.00M, 2, 3), new Api.Domain.Models.Product(2, "Product 2", 23.00M, 2, 5) };
            var paginatedResponse = new PagedResponse<Api.Domain.Models.Product>(products) { PageNumber = 1, PageSize = 10, NextPage = "http://example.com/?pageNumber=2&pageSize=10" };

            productServiceMock.Setup(mock => mock.GetAll(It.IsAny<PaginationFilter>())).Returns(Task.FromResult(Result.Success(products.AsEnumerable())));
            var expectedResponse = new OkObjectResult(paginatedResponse);

            // Act
            var result = await productController.Get(paginationQuery) as OkObjectResult;

            // Assert
            result.Value.Should().BeEquivalentTo(expectedResponse.Value);
        }

        [TestMethod]
        public async Task Get_InvalidPaginationQuery_ReturnsNotFoundResultWithError()
        {
            // Arrange
            var paginationQuery = new PaginationQuery { PageNumber = 0, PageSize = 10 };
            var error = "Invalid pagination parameters";
            var paginationFilter = new PaginationFilter(0, 10);

            productServiceMock.Setup(mock => mock.GetAll(It.IsAny<PaginationFilter>())).ReturnsAsync(Result.Failure<IEnumerable<Api.Domain.Models.Product>>(error));
            var expectedResponse = new NotFoundObjectResult(error);

            // Act
            var result = await productController.Get(paginationQuery);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task GetCheapest_ValidRequest_ReturnsOkResultWithListOfProducts()
        {
            // Arrange
            var products = new List<Api.Domain.Models.Product> { new Api.Domain.Models.Product(1, "Product 1", 12.00M, 2, 3), new Api.Domain.Models.Product(2, "Product 2", 23.00M, 2, 5) };

            productServiceMock.Setup(mock => mock.GetCheapest(10)).Returns(Task.FromResult(Result.Success(products.AsEnumerable())));
            var expectedResponse = new OkObjectResult(new Response<IEnumerable<Api.Domain.Models.Product>>(products));

            // Act
            var result = await productController.GetCheapest();

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task GetCheapest_InvalidRequest_ReturnsNotFoundResultWithError()
        {
            // Arrange
            var error = "Error retrieving cheapest products";

            productServiceMock.Setup(mock => mock.GetCheapest(10)).ReturnsAsync(Result.Failure<IEnumerable<Api.Domain.Models.Product>>(error));
            var expectedResponse = new NotFoundObjectResult(error);

            // Act
            var result = await productController.GetCheapest();

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task GetTopRanked_ValidRequest_ReturnsOkResultWithListOfProducts()
        {
            // Arrange
            var products = new List<Api.Domain.Models.Product> { new Api.Domain.Models.Product(1, "Product 1", 12.00M, 2, 3), new Api.Domain.Models.Product(2, "Product 2", 23.00M, 2, 5) };

            productServiceMock.Setup(mock => mock.GetTopRanked(100)).Returns(Task.FromResult(Result.Success(products.AsEnumerable())));
            var expectedResponse = new OkObjectResult(new Response<IEnumerable<Api.Domain.Models.Product>>(products));

            // Act
            var result = await productController.GetTopRanked();

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task GetTopRanked_InvalidRequest_ReturnsNotFoundResultWithError()
        {
            // Arrange
            var error = "Error retrieving top-ranked products";

            productServiceMock.Setup(mock => mock.GetTopRanked(100)).ReturnsAsync(Result.Failure<IEnumerable<Api.Domain.Models.Product>>(error));
            var expectedResponse = new NotFoundObjectResult(error);

            // Act
            var result = await productController.GetTopRanked();

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task Get_ValidId_ReturnsOkResultWithProduct()
        {
            // Arrange
            var id = 1;
            var product = new Api.Domain.Models.Product(1, "Product 1", 12.00M, 2, 3);

            productServiceMock.Setup(mock => mock.GetById(id)).ReturnsAsync(Result.Success(product));
            var expectedResponse = new OkObjectResult(new Response<Api.Domain.Models.Product>(product));

            // Act
            var result = await productController.Get(id);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task Get_InvalidId_ReturnsNotFoundResultWithError()
        {
            // Arrange
            var id = 1;
            var error = "Product not found";

            productServiceMock.Setup(mock => mock.GetById(id)).ReturnsAsync(Result.Failure<Api.Domain.Models.Product>(error));
            var expectedResponse = new NotFoundObjectResult(error);

            // Act
            var result = await productController.Get(id);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
