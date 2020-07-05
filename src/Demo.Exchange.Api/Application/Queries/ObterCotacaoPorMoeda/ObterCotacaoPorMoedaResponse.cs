namespace Demo.Exchange.Application.Queries.ObterCotacaoPorMoeda
{
    using Demo.Exchange.Application.Models;

    public class ObterCotacaoPorMoedaResponse : Response<CotacaoPorMoedaResponse>
    {
        public ObterCotacaoPorMoedaResponse(string requestId)
            : base(requestId)
        {
        }
    }
}