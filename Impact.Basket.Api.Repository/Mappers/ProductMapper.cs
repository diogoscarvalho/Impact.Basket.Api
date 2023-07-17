using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Repository.Dtos;

namespace Impact.Basket.Api.Repository.Mappers
{
    public static class ProductMapper
    {
        public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products)
        {
            return products.Select(product => product.ToDto());
        }

        public static IEnumerable<Product> ToDomain(this IEnumerable<ProductDto> productsDto)
        {
            return productsDto.Select(productDto => productDto.ToDomain());
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stars = product.Stars,
            };
        }

        public static Product ToDomain(this ProductDto productDto)
        {
            return new Product(productDto.Id,
                        productDto.Name,
                        productDto.Price,
                        productDto.Size,
                        productDto.Stars);
        }
    }
}
