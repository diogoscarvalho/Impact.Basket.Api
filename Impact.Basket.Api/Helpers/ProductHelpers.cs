using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Requests;
using Impact.Basket.Api.Models.Responses;

namespace Impact.Basket.Api.Helpers
{
    /// <summary>
    /// This class exists only to pull all products from the Code Challenge API and store in our repository
    /// In a real world situation it won't be done like this.
    /// </summary>
    public class ProductHelpers
    {
        private IProductService _productService;
        private ICodeChallengeApiService<OrderRequest, OrderResponse> _codeChallengeApiService;
        public ProductHelpers(IProductService productService, ICodeChallengeApiService<OrderRequest, OrderResponse> codeChallengeApiService)
        {
            _productService = productService;
            _codeChallengeApiService = codeChallengeApiService;
        }

        public async Task LoadAllProducts()
        {
            var products = await _codeChallengeApiService.GetAll();

            foreach (var product in products)
            {
                await _productService.Create(product);
            }
        }
    }
}
