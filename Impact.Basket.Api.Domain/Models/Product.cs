namespace Impact.Basket.Api.Domain.Models
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
        public int Id { get; }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Gets the size of the product.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the star rating of the product.
        /// </summary>
        public int Stars { get; }

        /// <summary>
        /// Initializes a new instance of the `Product` class with the specified ID, name, price, size, and star rating.
        ///
        /// <param name= "id"> The unique identifier of the product.</param>
        /// <param name= "name"> The name of the product.</param>
        /// <param name= "price"> The price of the product.</param>
        /// <param name= "size"> The size of the product.</param>
        /// <param name= "stars"> The star rating of the product.</param>
        /// </summary>
        public Product(int id, string name, decimal price, int size, int stars)
        {
            Id = id;
            Name = name;
            Price = price;
            Size = size;
            Stars = stars;
        }
    }
}
