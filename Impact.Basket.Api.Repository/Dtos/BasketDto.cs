namespace Impact.Basket.Api.Repository.Dtos
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
    }
}
