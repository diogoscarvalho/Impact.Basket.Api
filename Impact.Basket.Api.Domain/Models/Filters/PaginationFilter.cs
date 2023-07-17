namespace Impact.Basket.Api.Domain.Model.Filters
{
    /// <summary>
    /// Represents a pagination filter used for pagination queries.
    /// </summary>
    public class PaginationFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationFilter"/> class.
        /// </summary>
        public PaginationFilter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationFilter"/> class with the specified page number and page size.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }
    }

}
