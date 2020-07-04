namespace Demo.Exchange.Domain.AggregateModel.TaxaModel
{
    using Demo.Exchange.Domain.SeedWorks;
    using System;

    public class TaxaCobranca : Entity
    {
        private TaxaCobranca()
        {
        }

        public static TaxaCobranca EntidadeDefault() => new TaxaCobranca();

        public TaxaCobranca(string id, ValorTaxaCobranca valor, TipoSegmento tipoSegmento)
        {
            TaxaCobrancaId = id ?? throw new ArgumentException(nameof(id));
            ValorTaxa = valor ?? throw new ArgumentException(nameof(valor));
            TipoSegmento = tipoSegmento ?? throw new ArgumentException(nameof(tipoSegmento));

            AddDomainEvent(new NovaTaxaRegistradaEvent(this));
        }

        public string TaxaCobrancaId { get; private set; }
        public ValorTaxaCobranca ValorTaxa { get; private set; }
        public TipoSegmento TipoSegmento { get; private set; }
        public DateTime CriadoEm { get; } = DateTime.Now;
        public DateTime AtualizadoEm { get; private set; }

        public Result AtualizarTaxa(ValorTaxaCobranca novoValorCobranca)
        {
            if (ValorTaxa.Equals(novoValorCobranca))
                return Result.Fail("Não houve alteração do valor da taxa.");

            ValorTaxa = novoValorCobranca;
            AtualizadoEm = DateTime.Now;

            AddDomainEvent(new ValorTaxaAtualizadaEvent(TaxaCobrancaId));

            return Result.Ok();
        }
    }
}