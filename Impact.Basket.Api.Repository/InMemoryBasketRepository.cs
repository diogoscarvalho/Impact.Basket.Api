using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Repository.Dtos;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Impact.Basket.Api.Repository
{
    public class InMemoryBasketRepository : IGenericRepository<Domain.Models.Basket, Guid>
    {
        private ConcurrentDictionary<Guid, BasketDto> Baskets { get; } = new ConcurrentDictionary<Guid, BasketDto>();
        private ILogger<InMemoryBasketRepository> _logger;

        public InMemoryBasketRepository(ILogger<InMemoryBasketRepository> logger)
        {
            _logger = logger;
        }

        public Task<Result<Guid>> Create(Domain.Models.Basket entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Domain.Models.Basket>>> GetAll(PaginationFilter paginationFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Domain.Models.Basket>>> GetByFilter(Func<Domain.Models.Basket, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Domain.Models.Basket>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(Domain.Models.Basket entity)
        {
            throw new NotImplementedException();
        }
    }
}