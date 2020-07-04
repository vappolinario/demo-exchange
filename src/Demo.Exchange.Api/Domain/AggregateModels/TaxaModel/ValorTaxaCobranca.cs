namespace Demo.Exchange.Domain.AggregateModel.TaxaModel
{
    using Demo.Exchange.Domain.SeedWorks;
    using System.Collections.Generic;

    public class ValorTaxaCobranca : ValueObject
    {
        private ValorTaxaCobranca(decimal valor)
        {
            Valor = valor;
        }

        public static Result<ValorTaxaCobranca> Create(decimal valor)
        {
            if (valor < 0)
                return Result<ValorTaxaCobranca>.Fail("Valor da taxa não deve ser menor que zero.");

            return Result<ValorTaxaCobranca>.Ok(new ValorTaxaCobranca(valor));
        }

        public decimal Valor { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Valor;
        }
    }
}