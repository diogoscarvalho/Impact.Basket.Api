using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Helpers;
using Impact.Basket.Api.Mappers;
using Impact.Basket.Api.Models.Requests.Queries;
using Impact.Basket.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Impact.Basket.Api.Controllers
{
    /// <summary>
    /// The ProductController handles HTTP requests related to products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ILogger<ProductController> _logger;
        private readonly IUriService _uriService;
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the ProductController class with the specified logger, URI service, and product service.
        /// </summary>
        /// <param name="logger">The logger used for logging.</param>
        /// <param name="uriService">The URI service used for generating pagination links.</param>
        /// <param name="productService">The product service used for retrieving product data.</param>
        public ProductController(ILogger<ProductController> logger, IUriService uriService, IProductService productService)
        {
            _logger = logger;
            _uriService = uriService;
            _productService = productService;
        }

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="paginationQuery">The pagination query parameters.</param>
        /// <returns>The paginated list of products.</returns>
        [HttpGet]
        [ResponseCache(Duration = 600)]
        public async Task<IActionResult> Get([FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = paginationQuery.TopaginationFilter();
            var result = await _productService.GetAll(paginationFilter);

            return result.IsSuccess
                ? Ok(PaginationHelpers.CreatePaginatedResponse<Domain.Models.Product>(_uriService, paginationFilter, result.Value.ToList()))
                : NotFound(result.Error);
        }

        /// <summary>
        /// Retrieves a list of the cheapest products.
        /// </summary>
        /// <returns>The list of cheapest products.</returns>
        [HttpGet("/cheapest")]
        [ResponseCache(Duration = 100)]
        public async Task<IActionResult> GetCheapest()
        {
            // Hard coded number to fulfill the requirement, but we could receive it in a pagination query request or other parameter
            var result = await _productService.GetCheapest(10);

            return result.IsSuccess
                ? Ok(new Response<IEnumerable<Domain.Models.Product>>(result.Value))
                : NotFound(result.Error);
        }

        /// <summary>
        /// Retrieves a list of the top-ranked products.
        /// </summary>
        /// <returns>The list of top-ranked products.</returns>
        [HttpGet("/topRanked")]
        [ResponseCache(Duration = 50)]
        public async Task<IActionResult> GetTopRanked()
        {
            // Hard coded number to fulfill the requirement, but we could receive it in a pagination query request or other parameter
            var result = await _productService.GetTopRanked(100);

            return result.IsSuccess
                ? Ok(new Response<IEnumerable<Domain.Models.Product>>(result.Value))
                : NotFound(result.Error);
        }

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The specific product.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetById(id);

            return result.IsSuccess
                ? Ok(new Response<Domain.Models.Product>(result.Value))
                : NotFound(result.Error);
        }
    }
}
