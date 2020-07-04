namespace Demo.Exchange.Extensions.IoC
{
    using Demo.Exchange.Infra.Options;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal static class OptionsContainer
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EndPointConnectorsOptions>(configuration.GetSection(nameof(EndPointConnectorsOptions)));
            services.Configure<ConnectionStringOptions>(connectionStringOptions => connectionStringOptions.MySqlConnection = configuration.GetConnectionString("MySqlConnection"));
            return services;
        }
    }
}