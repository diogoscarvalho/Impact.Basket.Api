namespace Impact.Basket.Api.Domain.Models
{
    /// <summary>
    /// The `BasketItem` class represents an item in a basket.
    /// It holds information about the product, quantity, and total price of the item.
    /// </summary>
    public class BasketItem
    {
        private int _quantity;
        private decimal _totalPrice;

        /// <summary>
        /// Gets the product associated with the basket item.
        /// </summary>
        public Product Product { get; private set; }

        /// <summary>
        /// Gets the quantity of the basket item.
        /// </summary>
        public int Quantity => _quantity;

        /// <summary>
        /// Gets the total price of the basket item.
        /// </summary>
        public decimal TotalPrice => _totalPrice;

        /// <summary>
        /// Initializes a new instance of the `BasketItem` class with the specified product and quantity.
        /// </summary>
        ///<param name="product"> The product associated with the basket item.</param>
        ///<param name="quantity"> The quantity of the basket item.</param>
        public BasketItem(Product product, int quantity)
        {
            Product = product;
            _quantity = quantity;
            AdjustTotalPrice();
        }

        /// <summary>
        /// Increases the quantity of the basket item by 1 and adjusts the total price accordingly.
        /// </summary>
        public void IncreaseQuantity(int quantity)
        {
            _quantity += quantity;
            AdjustTotalPrice();
        }

        /// <summary>
        /// Decreases the quantity of the basket item by 1 and adjusts the total price accordingly.
        /// </summary>
        public void DecreaseQuantity()
        {
            if (_quantity > 0)
            {
                _quantity--;
                AdjustTotalPrice();
            }
        }

        /// <summary>
        /// Adjusts the total price of the basket item based on the product price and quantity.
        /// This method is called whenever the quantity is changed.
        /// </summary>
        private void AdjustTotalPrice()
        {
            _totalPrice = Product.Price * _quantity;
        }
    }
