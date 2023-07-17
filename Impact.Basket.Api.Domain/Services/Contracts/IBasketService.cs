using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Models;

namespace Impact.Basket.Api.Domain.Services.Contracts
{
    /// <summary>
    /// Represents a service for managing baskets.
    /// </summary>
    public interface IBasketService
    {
        /// <summary>
        /// Retrieves all baskets with optional pagination.
        /// </summary>
        /// <param name="pagination">The pagination filter. Default is null.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the collection of baskets if successful, or an error message if an error occurs.</returns>
        Task<Result<IEnumerable<Models.Basket>>> GetAll(PaginationFilter pagination = null);

        /// <summary>
        /// Retrieves a basket by its ID.
        /// </summary>
        /// <param name="id">The ID of the basket.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the basket if found, or an error message if the basket is not found or an error occurs.</returns>
        Task<Result<Models.Basket>> GetById(Guid id);

        /// <summary>
        /// Creates a new basket.
        /// </summary>
        /// <param name="basket">The basket to create.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the ID of the created basket if successful, or an error message if an error occurs.</returns>
        Task<Result<Guid>> Create(Models.Basket basket);

        /// <summary>
        /// Updates an existing basket.
        /// </summary>
        /// <param name="basket">The basket to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result"/> indicating if the update was successful or an error occurred.</returns>
        Task<Result> Update(Models.Basket basket);

        /// <summary>
        /// Deletes a basket by its ID.
        /// </summary>
        /// <param name="id">The ID of the basket to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result"/> indicating if the delete was successful or an error occurred.</returns>
        Task<Result> Delete(Guid id);

        /// <summary>
        /// Adds an item to a basket.
        /// </summary>
        /// <param name="basketId">The ID of the basket.</param>
        /// <param name="item">The item to add to the basket.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result"/> indicating if the item was added successfully or an error occurred.</returns>
        Task<Result> AddItem(Guid basketId, BasketItem item);

        /// <summary>
        /// Removes an item from a basket.
        /// </summary>
        /// <param name="basketId">The ID of the basket.</param>
        /// <param name="item">The item to remove from the basket.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result"/> indicating if the item was removed successfully or an error occurred.</returns>
        Task<Result> RemoveItem(Guid basketId, BasketItem item);

        /// <summary>
        /// Places an order for a basket.
        /// </summary>
        /// <param name="basketId">The ID of the basket to place an order for.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result"/> indicating if the order was placed successfully or an error occurred.</returns>
        Task<Result> PlaceAnOrder(Guid basketId);
    }

}
