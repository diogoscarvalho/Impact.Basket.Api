namespace Impact.Basket.Api.Models.Requests
{
    public class OrderRequest
    {
        public string UserEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public string ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
