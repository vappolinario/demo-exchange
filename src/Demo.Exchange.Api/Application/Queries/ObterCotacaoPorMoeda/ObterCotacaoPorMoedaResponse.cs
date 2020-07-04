namespace Demo.Exchange.Application.Queries.ObterCotacaoPorMoeda
{
    public class ObterCotacaoPorMoedaResponse : Response<CotacaoPorMoedaResponse>
    {
        public ObterCotacaoPorMoedaResponse(string requestId)
            : base(requestId)
        {
        }
    }

    public struct CotacaoPorMoedaResponse
    {
        public string MoedaDe { get; set; }
        public string MoedaPara { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorCotacao { get; set; }
    }

    public struct MoedaResponse
    {
        public string Moeda { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}