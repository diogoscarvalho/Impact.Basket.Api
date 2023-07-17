using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Models.Requests.Queries;

namespace Impact.Basket.Api.Mappers
{
    /// <summary>
    /// The PaginationQueryMapper class provides mapping methods for converting PaginationQuery objects to PaginationFilter objects.
    /// </summary>
    public static class PaginationQueryMapper
    {
        /// <summary>
        /// Converts a PaginationQuery object to the corresponding PaginationFilter object.
        /// </summary>
        /// <param name="paginationQuery">The PaginationQuery object to convert.</param>
        /// <returns>The corresponding PaginationFilter object.</returns>
        public static PaginationFilter TopaginationFilter(this PaginationQuery paginationQuery)
        {
            return new PaginationFilter(paginationQuery.PageNumber, paginationQuery.PageSize);
        }
    }
}
