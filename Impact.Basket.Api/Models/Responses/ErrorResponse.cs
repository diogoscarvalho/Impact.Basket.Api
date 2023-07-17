namespace Impact.Basket.Api.Models.Responses
{
    /// <summary>
    /// Object which is returned in case of an error in the application
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error description
        /// </summary>
        public string Message { get; set; }
    }
}
