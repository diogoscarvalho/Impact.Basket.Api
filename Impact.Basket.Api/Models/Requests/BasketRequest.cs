using Impact.Basket.Api.Domain.Models;

namespace Impact.Basket.Api.Models.Requests
{
    /// <summary>
    /// The BasketRequest class represents a request to create a basket.
    /// It contains a list of BasketItem objects.
    /// </summary>
    public class BasketRequest
    {
        /// <summary>
        /// Gets or sets the list of BasketItem objects.
        /// </summary>
        public List<BasketItem> Items { get; set; }
    }

    /// <summary>
    /// The BasketItem class represents an item within a basket.
    /// It contains a Product object and a quantity.
    /// </summary>
    public class BasketItem
    {
        /// <summary>
        /// Gets or sets the Product object associated with the basket item.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the basket item.
        /// </summary>
        public int Quantity { get; set; }
    }
}
