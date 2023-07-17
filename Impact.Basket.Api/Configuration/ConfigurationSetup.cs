using Impact.Basket.Api.Domain.Models;
using Impact.Basket.Api.Domain.Repositories.Contracts;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Repository;

namespace Impact.Basket.Api.Configuration
{
    /// <summary>
    /// Provides methods for setting up the configuration of services and repositories.
    /// </summary>
    public static class ConfigurationSetup
    {
        /// <summary>
        /// Adds services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();

            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");

                return new UriService(absoluteUri);
            });

            services.AddSingleton<ICodeChallengeApiService, CodeChallengeApiService>();

            return services;
        }

        /// <summary>
        /// Adds repositories to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IGenericRepository<Product, int>, InMemoryProductRepository>();
            services.AddSingleton<IGenericRepository<Domain.Models.Basket, Guid>, InMemoryBasketRepository>();

            return services;
        }
    }

}
