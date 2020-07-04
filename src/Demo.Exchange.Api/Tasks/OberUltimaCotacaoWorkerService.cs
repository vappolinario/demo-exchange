namespace Demo.Exchange.Tasks
{
    using Demo.Exchange.Infra.Connectors;
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public class OberUltimaCotacaoWorkerService : BackgroundService
    {
        private readonly IExchangeRatesApiConnector _exchangeRatesApiConnector;

        public OberUltimaCotacaoWorkerService(IExchangeRatesApiConnector exchangeRatesApiConnector)
        {
            _exchangeRatesApiConnector = exchangeRatesApiConnector;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var exchangeRates = await _exchangeRatesApiConnector.OberUltimaCotacaoPorMoeda("BRL");
                await Task.Delay(60000);
            }
        }
    }
}