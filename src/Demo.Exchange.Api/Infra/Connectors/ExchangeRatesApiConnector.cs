namespace Demo.Exchange.Infra.Connectors
{
    using Demo.Exchange.Infra.Options;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ExchangeRate
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }

    public interface IExchangeRatesApiConnector
    {
        Task<KeyValuePair<string, decimal>> OberUltimaCotacaoPorMoeda(string moeda);
    }

    public class ExchangeRatesApiConnector : Connector, IExchangeRatesApiConnector
    {
        public ExchangeRatesApiConnector(IHttpClientFactory httpClient, ILoggerFactory logger, IOptions<EndPointConnectorsOptions> endPointConnectorsOptions)
            : base(httpClient, logger.CreateLogger<ExchangeRatesApiConnector>(), endPointConnectorsOptions.Value.ExchangeRatesApiConnector)
        {
        }

        public async Task<KeyValuePair<string, decimal>> OberUltimaCotacaoPorMoeda(string moeda)
        {
            try
            {
                var endpoint = string.Concat(_connectorBaseUri.AbsoluteUri.TrimEnd('/'), $"{ConnectorRoutes.OberUltimaCotacaoPorMoeda(moeda)}");

                var client = _httpClient.CreateClient();
                var httpResponseMessage = await client.GetAsync(endpoint);

                httpResponseMessage.EnsureSuccessStatusCode();

                var content = await httpResponseMessage.Content.ReadAsStringAsync();

                var exchangeRate = JsonSerializer.Deserialize<ExchangeRate>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                if (!exchangeRate.Rates.Any(x => x.Key.Equals("BRL", StringComparison.InvariantCultureIgnoreCase)))
                    return default;

                return exchangeRate.Rates.FirstOrDefault(x => x.Key.Equals("BRL", StringComparison.InvariantCultureIgnoreCase));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}