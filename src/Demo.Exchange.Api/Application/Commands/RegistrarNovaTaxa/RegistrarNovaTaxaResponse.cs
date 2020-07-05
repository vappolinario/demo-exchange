namespace Demo.Exchange.Application.Commands.RegistrarNovaTaxa
{
    using Demo.Exchange.Application.Models;
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;

    public class RegistrarNovaTaxaResponse : Response<TaxaResponse>
    {
        public RegistrarNovaTaxaResponse(string requestId)
            : base(requestId)
        {
        }
    }

    public static class TaxaCobrancaEx
    {
        public static TaxaResponse ConverterEntidadeParaResponse(this TaxaCobranca taxaCobranca)
        {
            return new TaxaResponse
            {
                Id = taxaCobranca.TaxaCobrancaId,
                CriadoEm = taxaCobranca.CriadoEm,
                ValorTaxa = taxaCobranca.ValorTaxa.Valor,
                TipoSegmento = taxaCobranca.TipoSegmento.Id,
            };
        }
    }
}