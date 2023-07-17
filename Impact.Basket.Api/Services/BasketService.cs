using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Mappers;
using Impact.Basket.Api.Models.Requests;
using Impact.Basket.Api.Models.Responses;

namespace Impact.Basket.Api.Services
{
    /// <summary>
    /// The BasketService class provides operations for managing baskets.
    /// </summary>
    public class BasketService : IBasketService
    {
        private readonly ILogger<BasketService> _logger;
        private readonly IGenericRepository<Domain.Models.Basket, Guid> _basketRepository;
        private readonly ICodeChallengeApiService<OrderRequest, OrderResponse> _codeChallengeApiService;

        /// <summary>
        /// Initializes a new instance of the BasketService class with the specified logger and basket repository.
        /// </summary>
        /// <param name="logger">The logger used for logging.</param>
        /// <param name="basketRepository">The repository for accessing and manipulating baskets.</param>
        public BasketService(ILogger<BasketService> logger, IGenericRepository<Domain.Models.Basket, Guid> basketRepository, ICodeChallengeApiService<OrderRequest, OrderResponse> codeChallengeApiService)
        {
            _logger = logger;
            _basketRepository = basketRepository;
            _codeChallengeApiService = codeChallengeApiService;
        }

        /// <summary>
        /// Creates a new basket.
        /// </summary>
        /// <param name="basket">The basket to create.</param>
        /// <returns>The result of the create operation, containing the identity of the created basket.</returns>
        public async Task<Result<Guid>> Create(Domain.Models.Basket basket)
        {
            _logger.LogDebug("Creating a Basket with Identity {identity}", basket.Id);
            return await _basketRepository.Create(basket);
        }

        /// <summary>
        /// Deletes a basket by its identity.
        /// </summary>
        /// <param name="id">The identity of the basket to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public async Task<Result> Delete(Guid id)
        {
            _logger.LogDebug("Deleting a Basket with Identity {identity}", id);
            return await _basketRepository.Delete(id);
        }

        /// <summary>
        /// Retrieves a basket by its identity.
        /// </summary>
        /// <param name="id">The identity of the basket to retrieve.</param>
        /// <returns>The result of the retrieve operation, containing the retrieved basket.</returns>
        public async Task<Result<Domain.Models.Basket>> GetById(Guid id)
        {
            _logger.LogDebug("Retrieving a Basket with Identity {identity}", id);
            return await _basketRepository.GetById(id);
        }

        /// <summary>
        /// Updates a basket.
        /// </summary>
        /// <param name="basket">The basket to update.</param>
        /// <returns>The result of the update operation.</returns>
        public async Task<Result> Update(Domain.Models.Basket basket)
        {
            _logger.LogDebug("Updating a Basket with Identity {identity}", basket.Id);
            return await _basketRepository.Update(basket);
        }

        /// <summary>
        /// Adds an item to a basket.
        /// </summary>
        /// <param name="basketId">The identity of the basket.</param>
        /// <param name="item">The item to add to the basket.</param>
        /// <returns>The result of the add item operation.</returns>
        public async Task<Result> AddItem(Guid basketId, Domain.Models.BasketItem item)
        {
            var result = await _basketRepository.GetById(basketId);

            if (result.IsSuccess)
            {
                Domain.Models.Basket basket = result.Value;
                basket.AddItem(item);

                return await Update(basket);
            }

            return result;
        }

        /// <summary>
        /// Removes an item from a basket.
        /// </summary>
        /// <param name="basketId">The identity of the basket.</param>
        /// <param name="item">The item to remove from the basket.</param>
        /// <returns>The result of the remove item operation.</returns>
        public async Task<Result> RemoveItem(Guid basketId, Domain.Models.BasketItem item)
        {
            var result = await _basketRepository.GetById(basketId);

            if (result.IsSuccess)
            {
                Domain.Models.Basket basket = result.Value;
                basket.RemoveItem(item);

                return await Update(basket);
            }

            return result;
        }

        /// <summary>
        /// Retrieves all baskets with optional pagination.
        /// </summary>
        /// <param name="pagination">The pagination filter.</param>
        /// <returns>The result of the retrieve all operation, containing the list of baskets.</returns>
        public async Task<Result<IEnumerable<Domain.Models.Basket>>> GetAll(PaginationFilter pagination = null)
        {
            return await _basketRepository.GetAll(pagination);
        }

        /// <summary>
        /// Places an order based on the specified basket ID.
        /// </summary>
        /// <param name="basketId">The ID of the basket.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains the <see cref="Result"/>.</returns>
        public async Task<Result> PlaceAnOrder(Guid basketId)
        {
            var result = await GetById(basketId)
                .Ensure((basket) => basket.Status.Equals(BasketStatus.Open), "It is not possible to place an order to a Basket with status Ordered.")
                .Check((basket) => _codeChallengeApiService.CreateOrder(basket.ToOrder()))
                .Check((basket) => Result.Success<Domain.Models.Basket>(basket.CloseBasket())
                .Check((basket) => Update(basket)));

            return result;
        }
    }
}
