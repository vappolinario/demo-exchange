namespace Demo.Exchange.Extensions.IoC
{
    using Demo.Exchange.Application.Commands.RegistrarNovaTaxa;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    internal static class HandlersContainer
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(RegistrarNovaTaxaCommand).Assembly);
            return services;
        }
    }
}