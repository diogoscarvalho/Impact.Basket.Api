namespace Impact.Basket.Api.Models.Requests
{
    /// <summary>
    /// The `Product` class represents a product with its associated information.
    /// It contains properties for the product's ID, name, price, size, and star rating.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets the unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets the size of the product.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets the star rating of the product.
        /// </summary>
        public int Stars { get; set; }
    }
}
