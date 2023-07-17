using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Requests.Queries;
using Microsoft.AspNetCore.WebUtilities;

namespace Impact.Basket.Api.Services
{
    /// <summary>
    /// Represents a service for generating URIs.
    /// </summary>
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        /// <summary>
        /// Initializes a new instance of the UriService class with the specified base URI.
        /// </summary>
        /// <param name="baseUri">The base URI to use for generating URIs.</param>
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        /// <summary>
        /// Gets the URI for retrieving all products with optional pagination.
        /// </summary>
        /// <param name="paginationQuery">The pagination query parameters. Default is null.</param>
        /// <returns>The URI for retrieving all products.</returns>
        public Uri GetAllProductsUri(PaginationFilter paginationQuery = null)
        {
            var uri = new Uri(_baseUri);
            if (paginationQuery is null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, name: "pageNumber", value: paginationQuery.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, name: "pageSize", value: paginationQuery.PageSize.ToString());

            return new Uri(modifiedUri);
        }

        /// <summary>
        /// Gets the URI for retrieving a specific product by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>The URI for retrieving the specific product.</returns>
        public Uri GetProductUri(int productId)
        {
            return new Uri(_baseUri + $"api/Products/{productId}");
        }
    }
}
