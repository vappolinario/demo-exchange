namespace Demo.Exchange.Infra.Connectors
{
    using Demo.Exchange.Infra.Options;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Net.Http;

    public abstract class Connector
    {
        protected readonly Uri _connectorBaseUri;
        protected readonly IHttpClientFactory _httpClient;
        protected readonly IOptions<EndPointConnectorsOptions> _endPointConnectorsOptions;

        protected Connector(IHttpClientFactory httpClient, ILogger logger, string endPointConnector)
        {
            Logger = logger;
            _httpClient = httpClient;
            _connectorBaseUri = new Uri(endPointConnector);
        }

        protected ILogger Logger { get; }
    }
}