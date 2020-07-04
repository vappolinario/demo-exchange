namespace Demo.Exchange.Application.Queries.ObterTaxaCobrancaPorSegmento
{
    using Demo.Exchange.Application.Commands.RegistrarNovaTaxa;

    public class ObterTaxaCobrancaPorSegmentoResponse : Response<TaxaResponse>
    {
        public ObterTaxaCobrancaPorSegmentoResponse(string requestId)
            : base(requestId)
        {
        }
    }
}