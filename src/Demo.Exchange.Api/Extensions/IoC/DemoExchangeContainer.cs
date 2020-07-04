namespace Demo.Exchange.Extensions.IoC
{
    using Demo.Exchange.Infra.Cache.Memcached;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DemoExchangeContainer
    {
        public static IServiceCollection AddDemoExchangeServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();
            services.AddOptions(configuration);
            services.AddHandlers();
            services.AddRepositories();
            services.AddServices();
            services.AddMemcached(configuration);

            return services;
        }
    }
}