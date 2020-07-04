namespace Demo.Exchange.Extensions.IoC
{
    using Demo.Exchange.Domain.Services.CadernoFomulas;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServicesContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICadernoFormulasService, CadernoFormulasService>();

            return services;
        }
    }
}