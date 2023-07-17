using Impact.Basket.Api.Models.Requests;

namespace Impact.Basket.Api.Models.Responses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}
