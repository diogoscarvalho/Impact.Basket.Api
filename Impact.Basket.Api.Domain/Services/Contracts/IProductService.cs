using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Models;

namespace Impact.Basket.Api.Domain.Services.Contracts
{
    /// <summary>
    /// The <c>IProductService</c> interface provides operations for managing products in a basket.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products with optional pagination.
        /// </summary>
        /// <param name="pagination">The pagination filter. Default is null.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the collection of products if successful, or an error message if an error occurs.</returns>
        Task<Result<IEnumerable<Product>>> GetAll(PaginationFilter pagination = null);

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the product if it exists, or an error message if not found.</returns>
        Task<Result<Product>> GetById(int id);

        /// <summary>
        /// Creates a new product in the basket.
        /// </summary>
        /// <param name="entity">The product to create.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result"/> indicating success or failure of the operation.</returns>
        Task<Result<int>> Create(Product entity);

        /// <summary>
        /// Updates an existing product in the basket.
        /// </summary>
        /// <param name="entity">The product to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result"/> indicating success or failure of the operation.</returns>
        Task<Result> Update(Product entity);

        /// <summary>
        /// Deletes a product from the basket by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result"/> indicating success or failure of the operation.</returns>
        Task<Result> Delete(int id);

        /// <summary>
        /// Retrieves a specified number of products with the highest ranking.
        /// </summary>
        /// <param name="numberOfProducts">The number of products to retrieve.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the collection of products if successful, or an error message if an error occurs.</returns>
        Task<Result<IEnumerable<Product>>> GetTopRanked(int numberOfProducts);

        /// <summary>
        /// Retrieves a specified number of cheapest products.
        /// </summary>
        /// <param name="numberOfProducts">The number of products to retrieve.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the collection of products if successful, or an error message if an error occurs.</returns>
        Task<Result<IEnumerable<Product>>> GetCheapest(int numberOfProducts);
    }
