using Impact.Basket.Api.Models.Requests;

namespace Impact.Basket.Api.Mappers
{
    /// <summary>
    /// The BasketMapper class provides mapping methods for converting between BasketRequest and Basket objects.
    /// </summary>
    public static class BasketMapper
    {
        /// <summary>
        /// Converts a BasketRequest object to the corresponding Basket object.
        /// </summary>
        /// <param name="createBasket">The BasketRequest object to convert.</param>
        /// <returns>The corresponding Basket object.</returns>
        public static Domain.Models.Basket ToDomain(this BasketRequest createBasket)
        {
            var domainBasket = new Domain.Models.Basket();
            createBasket.Items.ForEach(item => domainBasket.AddItem(item.ToDomain()));

            return domainBasket;
        }

        /// <summary>
        /// Converts a BasketRequest object to the corresponding OrderRequest object.
        /// </summary>
        /// <param name="basket">The BasketRequest object to convert.</param>
        /// <returns>The corresponding Basket object.</returns>
        public static OrderRequest ToOrder(this Domain.Models.Basket basket)
        {
            return new OrderRequest
            {
                UserEmail = "diogo.carvalho@email.com", // It is hard-coded because our API doesn't have authentication, in real situation we would have it in the Authentication token
                OrderLines = basket.Items.Values.Select(item => item.ToOrderLine()).ToList(),
                TotalAmount = basket.Items.Values.Sum(x => x.TotalPrice)
            };
        }
    }
}
