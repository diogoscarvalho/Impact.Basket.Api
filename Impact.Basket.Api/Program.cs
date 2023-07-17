using Impact.Basket.Api.Configuration;
using Impact.Basket.Api.Domain.Services.Contracts;
using Impact.Basket.Api.Helpers;
using Impact.Basket.Api.Models.Requests;
using Impact.Basket.Api.Models.Responses;

namespace Impact.Basket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient();

            // Configuring DI
            builder.Services.AddServices();
            builder.Services.AddRepositories();
            builder.Services.AddControllers();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            // we are doing it here just to load all the producst from Code Challenge API when the application starts
            using (var scope = app.Services.CreateScope())
            {
                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                var codeChallengeService = scope.ServiceProvider.GetRequiredService<ICodeChallengeApiService<OrderRequest, OrderResponse>>();

                var productHelper = new ProductHelpers(productService, codeChallengeService);
                productHelper.LoadAllProducts();
            }

            app.Run();
        }
    }
}