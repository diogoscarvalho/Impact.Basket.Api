using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Repository.Dtos;
using Impact.Basket.Api.Repository.Mappers;
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

        public Task<Result<int>> Create(Product product)
        {
            _logger.LogDebug("Creating a new product with identity {productId}", product.Id);

            if (this.Products.ContainsKey(product.Id))
                return Task.FromResult(Result.Failure<int>($"Cannot insert product with identity '{product.Id}' as it already exists in the collection"));

            if (this.Products.TryAdd(product.Id, product.ToDto()))
                return Task.FromResult(Result.Success<int>(product.Id));

            return Task.FromResult(Result.Failure<int>($"Erro while saving product with identity {product.Id}"));
        }
        public Task<Result> Delete(int id)
        {
            if (!this.Products.ContainsKey(id))
            {
                return Task.FromResult(Result.Failure($"Cannot delete product with identity '{id}' as it doesn't exist"));
            }

            return Task.FromResult(Result.SuccessIf(Products.Remove(id, out _), $"Error while deleting product with identity {id}"));

        }
        public Task<Result<IEnumerable<Product>>> GetAll(PaginationFilter paginationFilter = null)
        {
            if (paginationFilter is null)
            {
                _logger.LogDebug("Retrieving all products at once");
                return Task.FromResult(Result.Success(this.Products.Values.ToDomain()));
            }

            _logger.LogDebug("Retrieving paged products with page number {pageNumber} and page size of {pageSize}", paginationFilter.PageNumber, paginationFilter.PageSize);

            return Task.FromResult(Result.Success(this.Products.Values.Skip((paginationFilter.PageNumber - 1 * paginationFilter.PageSize)).Take(paginationFilter.PageSize).ToDomain()));
        }

        public Task<Result<Product>> GetById(int id)
        {
            _logger.LogDebug($"FindMany Product with identity {id}");

            if (!this.Products.ContainsKey(id))
            {
                return Task.FromResult(Result.Failure<Product>("Product not found!"));
            }

            _logger.LogDebug($"Found Product with identity {id}");
            return Task.FromResult(Result.Success(this.Products[id].ToDomain()));
        }

        public Task<Result> Update(Product product)
        {
            if (!this.Products.ContainsKey(product.Id))
            {
                return Task.FromResult(Result.Failure($"Cannot update product with identity '{product.Id}' as it doesn't exist"));
            }

            Products[product.Id] = product.ToDto();
            _logger.LogDebug($"product with identity {product.Id} updated.");

            return Task.FromResult(Result.Success());
        }

        public Task<Result<IEnumerable<Product>>> GetByFilter(Func<Product, bool> predicate)
        {
            var result = this.Products.Values.ToDomain().Where(predicate);

            if (result.Any())
                return Task.FromResult(Result.Success(result));

            return Task.FromResult(Result.Failure<IEnumerable<Product>>("There are no products to the given predicate"));
        }
    }
}
