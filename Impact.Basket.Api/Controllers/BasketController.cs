using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Requests.Queries;
using Impact.Basket.Api.Models.Requests;
using Impact.Basket.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Impact.Basket.Api.Mappers;
using Impact.Basket.Api.Helpers;

namespace Impact.Basket.Api.Controllers
{
    /// <summary>
    /// The BasketController handles HTTP requests related to baskets.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IBasketService _basketService;
        private IUriService _uriService;

        /// <summary>
        /// Initializes a new instance of the BasketController class with the specified basket service and URI service.
        /// </summary>
        /// <param name="basketService">The basket service used for handling basket-related operations.</param>
        /// <param name="uriService">The URI service used for generating pagination links.</param>
        public BasketController(IBasketService basketService, IUriService uriService)
        {
            _basketService = basketService;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieves a paginated list of baskets.
        /// </summary>
        /// <param name="paginationQuery">The pagination query parameters.</param>
        /// <returns>The paginated list of baskets.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = paginationQuery.TopaginationFilter();

            var result = await _basketService.GetAll(paginationFilter);
            if (paginationFilter is null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return result.IsSuccess ? Ok(result) : NotFound(result.Error);
            }
            return result.IsSuccess
               ? Ok(PaginationHelpers.CreatePaginatedResponse<Domain.Models.Basket>(_uriService, paginationFilter, result.Value.ToList()))
               : NotFound(result.Error);
        }

        /// <summary>
        /// Retrieves a specific basket by its ID.
        /// </summary>
        /// <param name="id">The ID of the basket.</param>
        /// <returns>The specific basket.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _basketService.GetById(id);

            return result.IsSuccess
                ? Ok(new Response<Domain.Models.Basket>(result.Value))
                : NotFound(result.Error);
        }

        /// <summary>
        /// Creates a new basket.
        /// </summary>
        /// <param name="basketRequest">The BasketRequest object containing the basket information.</param>
        /// <returns>The result of the create operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BasketRequest basketRequest)
        {
            var result = await _basketService.Create(basketRequest.ToDomain());

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }

        /// <summary>
        /// Updates an existing basket.
        /// </summary>
        /// <param name="id">The ID of the basket to update.</param>
        /// <param name="basketRequest">The BasketRequest object containing the updated basket information.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] BasketRequest basketRequest)
        {
            var getBasketResult = await _basketService.GetById(id);

            if (getBasketResult.IsFailure)
                return NotFound(getBasketResult.Error);

            var existingBasket = getBasketResult.Value;

            existingBasket.UpdateWith(basketRequest.Items.ToDomain());

            var result = await _basketService.Update(existingBasket);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }

        /// <summary>
        /// Deletes a basket.
        /// </summary>
        /// <param name="id">The ID of the basket to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _basketService.Delete(id);

            return result.IsSuccess
                ? Ok(result)
                : NotFound(result.Error);
        }

        [HttpPost]
        [Route("placeOrder")]
        public async Task<IActionResult> PlaceAnOrder(Guid basketId)
        {
            var result = await _basketService.PlaceAnOrder(basketId);

            return result.IsSuccess
                ? Ok(result) : NotFound(result);
        }
    }
}
