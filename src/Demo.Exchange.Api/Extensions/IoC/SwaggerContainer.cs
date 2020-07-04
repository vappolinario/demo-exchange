namespace Demo.Exchange.Extensions.IoC
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    internal static class SwaggerContainer
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Itau.Exchange",
                    Version = "v1",
                    Description = "API por cotação e conversão de moeda estrangeira para reais.",
                });
            });

            return services;
        }
    }
}