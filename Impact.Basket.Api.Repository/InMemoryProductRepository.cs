using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Repository.Dtos;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Impact.Basket.Api.Repository
{
    public class InMemoryProductRepository : IGenericRepository<Product, int>
    {
        private ConcurrentDictionary<int, ProductDto> Products { get; } = new ConcurrentDictionary<int, ProductDto>();
        private ILogger<InMemoryProductRepository> _logger;

        public InMemoryProductRepository(ILogger<InMemoryProductRepository> logger)
        {
            _logger = logger;
        }

        public Task<Result<int>> Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Product>>> GetAll(PaginationFilter paginationFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Product>>> GetByFilter(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Product>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
