namespace Impact.Basket.Api.Domain.Models
{
    /// <summary>
    /// Represents the status of a basket.
    /// </summary>
    public enum BasketStatus
    {
        /// <summary>
        /// The basket is open and can be modified.
        /// </summary>
        Open = 0,

        /// <summary>
        /// The basket has been ordered and cannot be modified.
        /// </summary>
        Ordered = 1
    }

}
