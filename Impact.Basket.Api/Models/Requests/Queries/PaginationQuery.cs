namespace Impact.Basket.Api.Models.Requests.Queries
{
    /// <summary>
    /// Represents a pagination query for retrieving paginated data.
    /// </summary>
    public class PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the PaginationQuery class with default values.
        /// </summary>
        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 100;
        }

        /// <summary>
        /// Initializes a new instance of the PaginationQuery class with the specified page number and page size.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > 1000 ? 1000 : pageSize;
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
