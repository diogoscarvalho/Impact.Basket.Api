using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Models.Responses;

namespace Impact.Basket.Api.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter paginationFilter, List<T> response)
        {
            var nextPage = paginationFilter.PageNumber >= 1
                ? uriService.GetAllProductsUri(new PaginationFilter(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString()
                : null;

            var previousPage = paginationFilter.PageNumber - 1 >= 1
                ? uriService.GetAllProductsUri(new PaginationFilter(paginationFilter.PageNumber - 1, paginationFilter.PageSize)).ToString()
                : null;

            return new PagedResponse<T>
            {
                Data = response,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : null,
                NextPage = response.Count > 0 ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}
