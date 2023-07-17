namespace Impact.Basket.Api.Models.Responses
{
    /// <summary>
    /// The Response class represents a generic response containing a single data object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the data in the response.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Initializes a new instance of the Response class.
        /// </summary>
        public Response() { }

        /// <summary>
        /// Initializes a new instance of the Response class with the specified response data.
        /// </summary>
        /// <param name="response">The response data.</param>
        public Response(T response)
        {
            Data = response;
        }

        /// <summary>
        /// Gets or sets the data object in the response.
        /// </summary>
        public T Data { get; set; }
    }
}
