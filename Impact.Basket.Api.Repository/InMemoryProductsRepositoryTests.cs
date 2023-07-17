using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Domain.Repositories.Contracts;

namespace Impact.Basket.Api.Repository
{
    public class InMemoryProductsRepositoryTests : IGenericRepository<Product, int>
    {
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
