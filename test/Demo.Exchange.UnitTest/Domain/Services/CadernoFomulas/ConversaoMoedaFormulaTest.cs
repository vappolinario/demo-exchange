namespace Demo.Exchange.UnitTest.Domain.Services.CadernoFomulas
{
    using Demo.Exchange.Domain.Services.CadernoFomulas;
    using NUnit.Framework;
    using FluentAssertions;

    [TestFixture]
    public class ConversaoMoedaFormulaTest
    {
        [Test]
        public void Deve_Falhar_Calcular_Formula_Quantidade_Zerada()
        {
            const int quantidadeDesejada = 0;
            const decimal taxaCotacao = 5.3261M;
            const decimal taxaSegmento = 1M;

            var formula = new ConversaoMoedaFormula(new ConversaoMoedaParametroInput(quantidadeDesejada, taxaCotacao, taxaSegmento));
            var resultadoFormula = formula.Calcular();

            resultadoFormula.IsSuccess.Should().BeFalse();
            resultadoFormula.Messages.Should().HaveCountGreaterOrEqualTo(1);
        }

        [TestCase(1, 5.3261, 0, 5.33)]
        [TestCase(1, 5.3261, 1, 5.33)]
        [TestCase(1, 5.3261, 10, 53.30)]
        [TestCase(10, 5.3261, 10, 532.60)]
        public void Deve_Calcular_Sucesso_Formula(int quantidadeDesejada, decimal taxaCotacao, decimal taxaSegmento, decimal valorEsperado)
        {
            var formula = new ConversaoMoedaFormula(new ConversaoMoedaParametroInput(quantidadeDesejada, taxaCotacao, taxaSegmento));
            var resultadoFormula = formula.Calcular();

            resultadoFormula.IsSuccess.Should().BeTrue();
            resultadoFormula.IsFailure.Should().BeFalse();
            resultadoFormula.Messages.Should().HaveCount(0);
            resultadoFormula.Value.Valor.Should().Be(valorEsperado);
        }
    }
}