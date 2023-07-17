using Impact.Basket.Api.Domain.Model.Filters;

namespace Impact.Basket.Api.Domain.Services.Contracts
{
    /// <summary>
    /// Represents a service for generating URIs.
    /// </summary>
    public interface IUriService
    {
        /// <summary>
        /// Gets the URI for a specific product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>The URI for the specific product.</returns>
        Uri GetProductUri(int productId);

        /// <summary>
        /// Gets the URI for all products with optional pagination.
        /// </summary>
        /// <param name="paginationFilter">The pagination filter parameters. Default is null.</param>
        /// <returns>The URI for all products with optional pagination.</returns>
        Uri GetAllProductsUri(PaginationFilter paginationFilter = null);
    }

}
