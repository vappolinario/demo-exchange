namespace Demo.Exchange.UnitTest.Domain.AggregateModel.TaxaModel
{
    using FluentAssertions;
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class TaxaCobrancaTest
    {
        [Test]
        public void Deve_Criar_TaxaCobranca_Valida()
        {
            const decimal valorTaxa = 5;

            var novoId = Guid.NewGuid().ToString();
            var valorTaxaCobranaca = ValorTaxaCobranca.Create(valorTaxa);

            var novaTaxaCobranca = new TaxaCobranca(novoId, valorTaxaCobranaca.Value, TipoSegmento.Varejo);

            novaTaxaCobranca.TaxaCobrancaId.Should().Be(novoId);
            novaTaxaCobranca.ValorTaxa.Valor.Should().Be(valorTaxa);
        }

        [TestCase(5, 5, false)]
        [TestCase(5, 2, true)]
        public void Deve_Validar_Alteracao_ValueObject_ValorTaxaCobranca(decimal valorCorrente, decimal valorNovo, bool resultaEsperado)
        {
            var novoId = Guid.NewGuid().ToString();
            var valorTaxaCobranaca = ValorTaxaCobranca.Create(valorCorrente);
            var novaTaxaCobranca = new TaxaCobranca(novoId, valorTaxaCobranaca.Value, TipoSegmento.Varejo);

            var novoValorTaxaCobranaca = ValorTaxaCobranca.Create(valorNovo);

            var resutado = novaTaxaCobranca.AtualizarTaxa(novoValorTaxaCobranaca.Value);

            resutado.IsSuccess.Should().Be(resultaEsperado);
        }
    }
}