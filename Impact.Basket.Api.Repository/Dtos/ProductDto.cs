namespace Impact.Basket.Api.Repository.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Size { get; }
        public int Stars { get; set; }
    }
}
