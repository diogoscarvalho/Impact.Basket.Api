using Impact.Basket.Api.Models.Requests;

namespace Impact.Basket.Api.Mappers
{
    /// <summary>
    /// The BasketItemMapper class provides mapping methods for converting between different types of basket items.
    /// </summary>
    public static class BasketItemMapper
    {
        /// <summary>
        /// Converts a collection of BasketItem objects to the corresponding Domain.Models.BasketItem objects.
        /// </summary>
        /// <param name="items">The collection of BasketItem objects to convert.</param>
        /// <returns>The collection of corresponding Domain.Models.BasketItem objects.</returns>
        public static IEnumerable<Domain.Models.BasketItem> ToDomain(this IEnumerable<BasketItem> items)
        {
            return items.Select(item => item.ToDomain());
        }

        /// <summary>
        /// Converts a BasketItem object to the corresponding Domain.Models.BasketItem object.
        /// </summary>
        /// <param name="item">The BasketItem object to convert.</param>
        /// <returns>The corresponding Domain.Models.BasketItem object.</returns>
        public static Domain.Models.BasketItem ToDomain(this BasketItem item)
        {
            return new Domain.Models.BasketItem(item.Product.ToDomain(), item.Quantity);
        }

        /// <summary>
        /// Converts a <see cref="BasketItem"/> to an <see cref="OrderLine"/>.
        /// </summary>
        /// <param name="item">The <see cref="BasketItem"/> to convert.</param>
        /// <returns>The converted <see cref="OrderLine"/>.</returns>
        public static OrderLine ToOrderLine(this Domain.Models.BasketItem item)
        {
            return new OrderLine
            {
                ProductId = item.Product.Id,
                ProductName = item.Product.Name,
                ProductSize = item.Product.Size.ToString(),
                ProductUnitPrice = item.Product.Price,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice
            };
        }
    }
}
