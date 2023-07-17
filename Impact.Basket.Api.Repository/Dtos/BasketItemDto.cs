using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impact.Basket.Api.Repository.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public ProductDto Product { get; set; }
    }
}
