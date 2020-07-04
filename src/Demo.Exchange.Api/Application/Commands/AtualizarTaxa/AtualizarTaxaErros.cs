namespace Demo.Exchange.Application
{
    public static partial class Errors
    {
        public static class AtualizarTaxaErros
        {
            public static Error ValorTaxaSemAlteracao(decimal valorAtual, decimal novoValor)
                => new Error("ValorTaxaSemAlteracao", $"Para atualização da taxa o novo valor {novoValor} deve ser diferente do atual {valorAtual}");
        }
    }
}