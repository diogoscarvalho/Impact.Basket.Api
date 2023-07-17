using CSharpFunctionalExtensions;

namespace Impact.Basket.Api.Domain.Services.Contracts
{
    /// <summary>
    /// Represents a service for interacting with the Code Challenge API.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <typeparam name="TResult">The type of the result object.</typeparam>
    public interface ICodeChallengeApiService<TRequest, TResult>
        where TRequest : class
        where TResult : class
    {
        /// <summary>
        /// Retrieves all products from the Code Challenge API.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains the collection of products if successful, or an error message if an error occurs.</returns>
        Task<IEnumerable<Domain.Models.Product>> GetAll();

        /// <summary>
        /// Creates an order using the provided order request.
        /// </summary>
        /// <param name="orderRequest">The order request containing the order information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the order response if successful, or an error message if an error occurs.</returns>
        Task<Result<TResult>> CreateOrder(TRequest orderRequest);
    }


}
