using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Repository.Dtos;
using Impact.Basket.Api.Repository.Mappers;
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

        public Task<Result<IEnumerable<Domain.Models.Basket>>> GetAll(PaginationFilter paginationFilter = null)
        {
            if (paginationFilter is null)
            {
                _logger.LogDebug("Retrieving all Basket at once");
                return Task.FromResult(Result.Success(this.Baskets.Values.ToDomain()));
            }

            _logger.LogDebug("Retrieving paged baskets with page number {pageNumber} and page size of {pageSize}", paginationFilter.PageNumber, paginationFilter.PageSize);

            return Task.FromResult(Result.Success(this.Baskets.Values.Skip((paginationFilter.PageNumber - 1 * paginationFilter.PageSize)).Take(paginationFilter.PageSize).ToDomain()));
        }

        public Task<Result<Guid>> Create(Domain.Models.Basket basket)
        {
            _logger.LogDebug("Creating a new basket with identity {basketId}", basket.Id);

            if (this.Baskets.ContainsKey(basket.Id))
                return Task.FromResult(Result.Failure<Guid>($"Cannot insert basket with identity '{basket.Id}' as it already exists in the collection"));

            if (this.Baskets.TryAdd(basket.Id, basket.ToDto()))
                return Task.FromResult(Result.Success(basket.Id));

            return Task.FromResult(Result.Failure<Guid>($"Erro while saving basket with identity {basket.Id}"));

        }

        public Task<Result<IEnumerable<Domain.Models.Basket>>> GetAll()
        {
            return Task.FromResult(Result.Success(this.Baskets.Values.ToDomain()));
        }

        public Task<Result<Domain.Models.Basket>> GetById(Guid id)
        {
            _logger.LogDebug($"FindMany Basket with identity {id}");

            if (!this.Baskets.ContainsKey(id))
            {
                return Task.FromResult(Result.Failure<Domain.Models.Basket>($"Basket with identity {id} not found!"));
            }

            _logger.LogDebug($"Found Basket with identity {id}");
            return Task.FromResult(Result.Success(this.Baskets[id].ToDomain()));
        }

        public Task<Result> Update(Domain.Models.Basket basket)
        {
            if (!this.Baskets.ContainsKey(basket.Id))
            {
                return Task.FromResult(Result.Failure($"Cannot update basket with identity '{basket.Id}' as it doesn't exist"));
            }

            Baskets[basket.Id] = basket.ToDto();
            _logger.LogDebug($"Basket with identity {basket.Id} updated.");

            return Task.FromResult(Result.Success());
        }

        public Task<Result> Delete(Guid id)
        {
            if (!this.Baskets.ContainsKey(id))
            {
                return Task.FromResult(Result.Failure($"Cannot delete basket with identity '{id}' as it doesn't exist"));
            }

            return Task.FromResult(Result.SuccessIf(Baskets.Remove(id, out _), $"Error while deleting basket with identity {id}"));
        }

        public Task<Result<IEnumerable<Domain.Models.Basket>>> GetByFilter(Func<Domain.Models.Basket, bool> predicate)
        {
            var result = this.Baskets.Values.ToDomain().Where(predicate);

            if (result.Any())
                return Task.FromResult(Result.Success(result));

            return Task.FromResult(Result.Failure<IEnumerable<Domain.Models.Basket>>("There are no baskets to the given predicate"));
        }
    }
}