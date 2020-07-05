namespace Demo.Exchange.Api.Controllers.v1
{
    using Demo.Exchange.Application.Models;
    using Demo.Exchange.Application.Queries.ObterCotacaoPorMoeda;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;

    [ApiController]
    [ApiVersion(API_VERSION)]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/cotacoes")]
    public class CotacoesController : Controller
    {
        private const string API_VERSION = "1";
        private readonly IMediator _mediator;

        public CotacoesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CotacaoPorMoedaResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterCotacaoPorMoeda([FromQuery] string segmento, [FromQuery] string moeda, [FromQuery] decimal quantidade)
        {
            var response = await _mediator.Send(new ObterCotacaoPorMoedaQuery(segmento, moeda, quantidade));
            if (response.IsFailure)
                return BadRequest(response.ErrorResponse);

            return Ok(response.PayLoad);
        }
    }
}