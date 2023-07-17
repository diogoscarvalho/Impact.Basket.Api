using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Domain.Services.Contracts;

namespace Impact.Basket.Api.Services
{
    /// <summary>
    /// The ProductService class provides operations for managing products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Domain.Models.Product, int> _repository;
        private ILogger<ProductService> _logger;

        /// <summary>
        /// Initializes a new instance of the ProductService class with the specified repository and logger.
        /// </summary>
        /// <param name="repository">The repository for accessing and manipulating products.</param>
        /// <param name="logger">The logger used for logging.</param>
        public ProductService(IGenericRepository<Domain.Models.Product, int> repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The result of the create operation, containing the identity of the created product.</returns>
        public async Task<Result<int>> Create(Domain.Models.Product product)
        {
            _logger.LogDebug("Creating a {name} product with identity {id}", product.Name, product.Id);
            return await _repository.Create(product);
        }

        /// <summary>
        /// Deletes a product by its identity.
        /// </summary>
        /// <param name="id">The identity of the product to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public async Task<Result> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        /// <summary>
        /// Retrieves all products with optional pagination.
        /// </summary>
        /// <param name="pagination">The pagination filter.</param>
        /// <returns>The result of the retrieve all operation, containing the list of products.</returns>
        public async Task<Result<IEnumerable<Domain.Models.Product>>> GetAll(PaginationFilter pagination = null)
        {
            return await _repository.GetAll(pagination);
        }

        /// <summary>
        /// Retrieves a product by its identity.
        /// </summary>
        /// <param name="id">The identity of the product to retrieve.</param>
        /// <returns>The result of the retrieve operation, containing the retrieved product.</returns>
        public async Task<Result<Domain.Models.Product>> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        /// <summary>
        /// Retrieves the specified number of cheapest products.
        /// </summary>
        /// <param name="numberOfProducts">The number of products to retrieve.</param>
        /// <returns>The result of the retrieve operation, containing the list of cheapest products.</returns>
        public async Task<Result<IEnumerable<Domain.Models.Product>>> GetCheapest(int numberOfProducts)
        {
            var allProductsResult = await _repository.GetAll();

            return allProductsResult.IsSuccess ? Result.Success(allProductsResult.Value.OrderBy(product => product.Price).Take(numberOfProducts))
                : allProductsResult;
        }

        /// <summary>
        /// Retrieves the specified number of top-ranked products.
        /// </summary>
        /// <param name="numberOfProducts">The number of products to retrieve.</param>
        /// <returns>The result of the retrieve operation, containing the list of top-ranked products.</returns>
        public async Task<Result<IEnumerable<Domain.Models.Product>>> GetTopRanked(int numberOfProducts)
        {
            var allProductsResult = await _repository.GetAll();

            return allProductsResult.IsSuccess ? Result.Success(allProductsResult.Value.OrderByDescending(product => product.Stars).Take(numberOfProducts))
               : allProductsResult;
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The result of the update operation.</returns>
        public async Task<Result> Update(Domain.Models.Product product)
        {
            return await _repository.Update(product);
        }
    }
}
