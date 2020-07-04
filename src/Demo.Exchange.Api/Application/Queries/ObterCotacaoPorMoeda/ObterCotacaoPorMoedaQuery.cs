namespace Demo.Exchange.Application.Queries.ObterCotacaoPorMoeda
{
    using MediatR;

    public class ObterCotacaoPorMoedaQuery : Request, IRequest<ObterCotacaoPorMoedaResponse>
    {
        public ObterCotacaoPorMoedaQuery(string segmento, string moeda, decimal quantidade)
        {
            Segmento = segmento;
            Moeda = moeda;
            Quantidade = quantidade;
        }

        public string Segmento { get; }
        public string Moeda { get; }
        public decimal Quantidade { get; }

        public override Response Response => new ObterCotacaoPorMoedaResponse(RequestId);
    }
}