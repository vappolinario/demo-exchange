namespace Demo.Exchange.Application.Queries.ObterTaxaCobrancaPorSegmento
{
    using Demo.Exchange.Application.Models;

    public class ObterTaxaCobrancaPorSegmentoResponse : Response<TaxaResponse>
    {
        public ObterTaxaCobrancaPorSegmentoResponse(string requestId)
            : base(requestId)
        {
        }
    }
}