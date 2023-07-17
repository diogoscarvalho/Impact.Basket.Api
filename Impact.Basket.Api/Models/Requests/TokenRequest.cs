namespace Impact.Basket.Api.Models.Requests
{
    /// <summary>
    /// The TokenRequest class represents a request for an access token.
    /// It contains the email associated with the request.
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Gets or sets the email for the access token request.
        /// </summary>
        public string Email { get; set; }
    }
}
