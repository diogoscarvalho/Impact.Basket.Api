namespace Impact.Basket.Api.Models.Responses
{
    public class PagedResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the PagedResponse class.
        /// </summary>
        public PagedResponse() { }

        /// <summary>
        /// Initializes a new instance of the PagedResponse class with the specified collection of responses.
        /// </summary>
        /// <param name="responses">The collection of responses.</param>
        public PagedResponse(IEnumerable<T> responses)
        {
            Data = responses;
        }

        /// <summary>
        /// Gets or sets the collection of data in the response.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the page number of the response.
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the page size of the response.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Gets or sets the link to the next page.
        /// </summary>
        public string NextPage { get; set; }

        /// <summary>
        /// Gets or sets the link to the previous page.
        /// </summary>
        public string PreviousPage { get; set; }
    }
}
