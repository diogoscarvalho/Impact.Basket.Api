using Impact.Basket.Api.Services;
using Microsoft.Extensions.Logging;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using System.Net;
using Impact.Basket.Api.Domain.Models;

namespace Impact.Basket.Api.UnitTests.Services
{
    [TestClass]
    public class CodeChallengeApiServiceTests
    {
        private Mock<IHttpClientFactory> _httpClientFactoryMock;
        private Mock<ILogger<CodeChallengeApiService>> _loggerMock;
        private CodeChallengeApiService _apiService;

        [TestInitialize]
        public void Initialize()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _loggerMock = new Mock<ILogger<CodeChallengeApiService>>();
            _apiService = new CodeChallengeApiService(_httpClientFactoryMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetAll_ValidResponse_ReturnsListOfProducts()
        {
            // Arrange
            var accessToken = "dummyAccessToken";
            var products = new List<Product>
            {
                new Product(1, "Product 1", 12.00M, 2, 3),
                new Product(2, "Product 2", 19.90M, 2, 4),
                new Product(5, "Product 5", 88.90M, 2, 1)
            };

            var baseUri = new Uri("https://dymmy-uri/api");

            var httpClientHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpClientHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(products))
                });

            httpClientHandlerMock.Protected()
                .Setup("Dispose", ItExpr.IsAny<bool>());

            var httpClientMock = new HttpClient(httpClientHandlerMock.Object)
            {
                BaseAddress = baseUri
            };

            _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClientMock);

            // Act
            var result = await _apiService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(products.Count, result.Count());
            CollectionAssert.AreEquivalent(products, result.ToList());
        }

        [TestMethod]
        public async Task GetAll_UnauthorizedResponse_RetriesAfterSettingAccessToken()
        {
            // Arrange
            var accessToken = "dummyAccessToken";
            var products = new List<Product>
            {
                new Product(1, "Product 1", 12.00M, 2, 3),
                new Product(2, "Product 2", 19.90M, 2, 4),
                new Product(5, "Product 5", 88.90M, 2, 1)
            };

            var unauthorizedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized
            };

            var httpClientHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpClientHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(products))
                });

            var httpClientMock = new HttpClient(httpClientHandlerMock.Object);
            _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClientMock);

            // Act
            var result = await _apiService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(products.Count, result.Count());
            CollectionAssert.AreEquivalent(products, result.ToList());
        }
    }
}
