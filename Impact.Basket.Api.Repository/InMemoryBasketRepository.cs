using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;
using Impact.Basket.Api.Domain.Repositories.Contracts;

namespace Impact.Basket.Api.Repository
{
    public class InMemoryBasketRepository : IGenericRepository<Domain.Models.Basket, Guid>
    {
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