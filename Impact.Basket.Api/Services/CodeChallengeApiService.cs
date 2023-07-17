using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Requests;
using Impact.Basket.Api.Models.Responses;
using Newtonsoft.Json;
using System.Net;

namespace Impact.Basket.Api.Services
{
    /// <summary>
    /// Represents a service for interacting with the Code Challenge API.
    /// </summary>
    public class CodeChallengeApiService : ICodeChallengeApiService<OrderRequest, OrderResponse>
    {
        private ILogger<CodeChallengeApiService> _logger;
        private IHttpClientFactory _httpClientFactory;
        private const string _baseUri = "https://azfun-impact-code-challenge-api.azurewebsites.net/api/";
        private readonly string clientKey = "diogo.carvalho@email.com";
        private string _accessToken;

        /// <summary>
        /// Initializes a new instance of the CodeChallengeApiService class with the specified HTTP client factory and logger.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory used to create HTTP clients.</param>
        /// <param name="logger">The logger used for logging.</param>
        public CodeChallengeApiService(IHttpClientFactory httpClientFactory, ILogger<CodeChallengeApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all products from the Code Challenge API.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains the collection of products if successful, or an error message if an error occurs.</returns>
        public async Task<IEnumerable<Domain.Models.Product>> GetAll()
        {
            _logger.LogDebug("Getting all the products from the Code Challenge API");

            var result = Enumerable.Empty<Domain.Models.Product>();
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                if (string.IsNullOrWhiteSpace(_accessToken))
                    await SetApiAccessToken(clientKey);

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                var response = await httpClient.GetAsync($"{_baseUri}/GetAllProducts");

                if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    await SetApiAccessToken(clientKey);
                    return await GetAll();
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    _logger.LogDebug("Got all the products from the Code Challenge API");

                    return JsonConvert.DeserializeObject<List<Domain.Models.Product>>(responseString);
                }
            }
        }

        /// <summary>
        /// Creates an order using the provided order request.
        /// </summary>
        /// <param name="orderRequest">The order request containing the order information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the order response if successful, or an error message if an error occurs.</returns>
        public async Task<Result<OrderResponse>> CreateOrder(OrderRequest orderRequest)
        {
            _logger.LogDebug("Requesting to create an order for the client {clientEmail}", orderRequest.UserEmail);
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                if (string.IsNullOrWhiteSpace(_accessToken))
                    await SetApiAccessToken(clientKey);

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
                string json = JsonConvert.SerializeObject(orderRequest);

                var response = await httpClient.PostAsync($"{_baseUri}/CreateOrder", new StringContent(json));

                if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    await SetApiAccessToken(clientKey);
                    return await CreateOrder(orderRequest);
                }
                else if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    _logger.LogDebug("Order for the client {clientEmail} created!", orderRequest.UserEmail);

                    var responseString = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<OrderResponse>(responseString);
                }

                return Result.Failure<OrderResponse>($"Error while creating an order for the client {orderRequest.UserEmail}");
            }
        }

        private async Task SetApiAccessToken(string email)
        {
            _logger.LogDebug("Requesting access token");
            var tokenRequest = new TokenRequest { Email = email };
            string json = JsonConvert.SerializeObject(tokenRequest);
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.PostAsync($"{_baseUri}/Login", new StringContent(json));

                if (response.IsSuccessStatusCode)
                {
                    var token = JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());
                    _accessToken = token.Token;
                }
                else
                {
                    _accessToken = string.Empty;
                }
            }
        }
    }

}
