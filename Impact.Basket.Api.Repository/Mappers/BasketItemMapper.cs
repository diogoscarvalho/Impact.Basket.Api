using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impact.Basket.Api.Repository.Mappers
{
    public static class BasketItemMapper
    {
        public static IEnumerable<BasketItemDto> ToDto(this IEnumerable<BasketItem> basketItems)
        {
            return basketItems.Select(item => item.ToDto());
        }

        public static BasketItemDto ToDto(this BasketItem basketItem)
        {
            return new BasketItemDto
            {
                Quantity = basketItem.Quantity,
                Product = basketItem.Product.ToDto()
            };
        }

        public static IEnumerable<BasketItem> ToDomain(this IEnumerable<BasketItemDto> basketItemsDto)
        {
            return basketItemsDto.Select(item => item.ToDomain());
        }

        public static BasketItem ToDomain(this BasketItemDto basketItem)
        {
            return new BasketItem(basketItem.Product.ToDomain(), basketItem.Quantity);
        }
    }
}
