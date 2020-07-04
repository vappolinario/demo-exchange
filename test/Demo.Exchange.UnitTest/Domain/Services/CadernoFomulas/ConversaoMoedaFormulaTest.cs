namespace Demo.Exchange.UnitTest.Domain.Services.CadernoFomulas
{
    using Demo.Exchange.Domain.Services.CadernoFomulas;
    using NUnit.Framework;

    [TestFixture]
    public class ConversaoMoedaFormulaTest
    {
        [Test]
        public void Teste()
        {
            var parametroInput = new ConversaoMoedaParametroInput(0, 5.3261M, 1M);
            var formula = new ConversaoMoedaFormula(parametroInput);

            var resultado = formula.Calcular();
        }
    }
}
