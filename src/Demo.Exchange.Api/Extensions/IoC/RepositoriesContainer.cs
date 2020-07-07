namespace Demo.Exchange.Extensions.IoC
{
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using Demo.Exchange.Infra.Connectors;
    using Demo.Exchange.Infra.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    internal static class RepositoriesContainer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddConnectors();
            services.AddScoped<ITaxaCobrancaRepository, TaxaCobrancaRepository>();
            return services;
        }

        private static IServiceCollection AddConnectors(this IServiceCollection services)
        {
            services.AddScoped<IExchangeRatesApiConnector, ExchangeRatesApiConnector>();
            return services;
        }
    }
}