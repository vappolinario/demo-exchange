namespace Demo.Exchange.Application.Models
{
    public struct CotacaoPorMoedaResponse
    {
        public MoedaResponse MoedaDe { get; set; }
        public MoedaResponse MoedaPara { get; set; }
        public decimal TaxaConversao { get; set; }
        public decimal QuantidadeDesejada { get; set; }
        public decimal ValorCotacao { get; set; }
    }

    public struct MoedaResponse
    {
        public string Moeda { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}