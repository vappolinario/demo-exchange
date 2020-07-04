namespace Demo.Exchange.Infra.Connectors
{
    public static partial class ConnectorRoutes
    {
        public static string OberUltimaCotacao() => "/latest";

        public static string OberUltimaCotacaoPorMoeda(string moeda) => $"/latest?base={moeda}";
    }
}