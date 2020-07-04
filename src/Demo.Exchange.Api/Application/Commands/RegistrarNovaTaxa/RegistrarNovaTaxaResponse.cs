using Demo.Exchange.Domain.AggregateModel.TaxaModel;
using System;

namespace Demo.Exchange.Application.Commands.RegistrarNovaTaxa
{
    public struct TaxaResponse
    {
        public string Id { get; set; }
        public string TipoSegmento { get; set; }
        public decimal ValorTaxa { get; set; }
        public DateTime CriadoEm { get; set; }
    }

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
                TipoSegmento = taxaCobranca.TipoSegmento.DesricaoSimples,
            };
        }
    }
}