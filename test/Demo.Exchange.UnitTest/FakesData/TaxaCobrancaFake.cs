namespace Demo.Exchange.UnitTest.FakesData
{
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using System;

    public static partial class FakeData
    {
        private const int VALOR_UM = 1;
        private const int VALOR_DEZ = 10;
        private const int VALOR_100 = 100;

        public static TaxaCobranca TaxaCobrancaNula => TaxaCobranca.EntidadeDefault();

        public static TaxaCobranca TaxaCobrancaValidaVarejoValorUm => new TaxaCobranca(Guid.NewGuid().ToString(), ValorTaxaCobrancaValorUm, TipoSegmento.Varejo);

        public static ValorTaxaCobranca ValorTaxaCobrancaValorUm 
        {
            get 
            {
                var valorTaxaCobranca = ValorTaxaCobranca.Create(VALOR_UM);
                return valorTaxaCobranca.Value;
            }
        }
    }
}