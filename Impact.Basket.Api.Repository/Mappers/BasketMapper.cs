using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Repository.Dtos;
using System.Collections.Concurrent;

namespace Impact.Basket.Api.Repository.Mappers
{
    public static class BasketMapper
    {
        public static IEnumerable<BasketDto> ToDto(this IEnumerable<Domain.Models.Basket> baskets)
        {
            return baskets.Select(basket => basket.ToDto());
        }

        public static IEnumerable<Domain.Models.Basket> ToDomain(this IEnumerable<BasketDto> basketsDto)
        {
            return basketsDto.Select(basket => basket.ToDomain());
        }
        public static BasketDto ToDto(this Domain.Models.Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                Status = (int)basket.Status,
                Items = basket.Items.Values.ToDto()
            };
        }

        public static Domain.Models.Basket ToDomain(this BasketDto basketDto)
        {
            return new Domain.Models.Basket(basketDto.Id,
                        new ConcurrentDictionary<int, BasketItem>(basketDto.Items.ToDomain().ToDictionary(item => item.Product.Id)),
                        (BasketStatus)basketDto.Status);
        }
    }
}
