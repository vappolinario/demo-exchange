namespace Demo.Exchange.UnitTest.Domain.AggregateModel.TaxaModel
{
    using FluentAssertions;
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using NUnit.Framework;

    [TestFixture]
    public class ValorTaxaCobrancaTest
    {
        [Test]
        public void Deve_Falhar_Criacao_ValorTaxaCobranca_Quando_Valor_For_Invalido()
        {
            const decimal valorTaxa = -1;
            var taxa = ValorTaxaCobranca.Create(valorTaxa);

            taxa.Should().NotBeNull();
            taxa.IsSuccess.Should().BeFalse();
            taxa.IsFailure.Should().BeTrue();
        }

        [Test]
        public void Deve_Criar_ValorTaxaCobranca_Sucesso()
        {
            const decimal valorTaxa = 5;
            var taxa = ValorTaxaCobranca.Create(valorTaxa);

            taxa.Should().NotBeNull();
            taxa.IsSuccess.Should().BeTrue();
            taxa.Value.Valor.Should().Be(valorTaxa);
        }

        [TestCase(5, 5, true)]
        [TestCase(5, 2.1, false)]
        public void Deve_Validar_Alteracaoes_Entre_Objetos(decimal valorCorrente, decimal valorNovo, bool resultaEsperado)
        {
            var taxaNovo = ValorTaxaCobranca.Create(valorNovo);
            var taxaCorrente = ValorTaxaCobranca.Create(valorCorrente);

            var validarTaxaTrocou = taxaCorrente.Value.Equals(taxaNovo.Value);

            validarTaxaTrocou.Should().Be(resultaEsperado);
        }
    }
}