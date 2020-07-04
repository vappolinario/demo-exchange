namespace Demo.Exchange.Application
{
    public static partial class Errors
    {
        public static class RegistrarNovaTaxaErros
        {
            public static Error TaxaParaSegmentoJaRegistrada(string segmento)
                => new Error("TaxaParaSegmentoJaRegistrada", $"Taxa para o segmento {segmento} já registrado");
        }
    }
}