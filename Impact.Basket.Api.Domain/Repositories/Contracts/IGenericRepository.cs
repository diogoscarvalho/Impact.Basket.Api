using CSharpFunctionalExtensions;
using Impact.Basket.Api.Domain.Model.Filters;

namespace Impact.Basket.Api.Domain.Repositories.Contracts
{
    /// <summary>
    /// Represents a generic repository for performing CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TIdentityType">The type of the identity property of the entity.</typeparam>
    public interface IGenericRepository<TEntity, TIdentityType> where TEntity : class
    {
        /// <summary>
        /// Retrieves all entities with optional pagination.
        /// </summary>
        /// <param name="paginationFilter">The pagination filter parameters.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of the retrieval operation.</returns>
        Task<Result<IEnumerable<TEntity>>> GetAll(PaginationFilter paginationFilter = null);

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of the retrieval operation.</returns>
        Task<Result<TEntity>> GetById(TIdentityType id);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of the create operation.</returns>
        Task<Result<TIdentityType>> Create(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of the update operation.</returns>
        Task<Result> Update(TEntity entity);

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of the delete operation.</returns>
        Task<Result> Delete(TIdentityType id);

        /// <summary>
        /// Retrieves entities that match the specified filter predicate.
        /// </summary>
        /// <param name="predicate">The filter predicate.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of the retrieval operation.</returns>
        Task<Result<IEnumerable<TEntity>>> GetByFilter(Func<TEntity, bool> predicate);
    }

}
