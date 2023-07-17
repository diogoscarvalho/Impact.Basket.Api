using Impact.Basket.Api.Models.Requests;

namespace Impact.Basket.Api.Mappers
{
    /// <summary>
    /// The ProductMapper class provides mapping methods for converting Product objects to Domain.Models.Product objects.
    /// </summary>
    public static class ProductMapper
    {
        /// <summary>
        /// Converts a Product object to the corresponding Domain.Models.Product object.
        /// </summary>
        /// <param name="product">The Product object to convert.</param>
        /// <returns>The corresponding Domain.Models.Product object.</returns>
        public static Domain.Models.Product ToDomain(this Product product)
        {
            return new Domain.Models.Product(product.Id,
                product.Name,
                product.Price,
                product.Size,
                product.Stars);
        }
    }
}
