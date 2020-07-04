namespace Demo.Exchange.Application.Queries.ObterTaxaCobrancaPorSegmento
{
    using Demo.Exchange.Application.Commands.RegistrarNovaTaxa;
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using Demo.Exchange.Infra.Cache.Memcached;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    public class ObterTaxaCobrancaPorSegmentoHandler : Handler, IRequestHandler<ObterTaxaCobrancaPorSegmentoQuery, ObterTaxaCobrancaPorSegmentoResponse>
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly ITaxaCobrancaRepository _taxaCobrancaRepository;

        public ObterTaxaCobrancaPorSegmentoHandler(IMediator mediator, ILoggerFactory logger, ITaxaCobrancaRepository taxaCobrancaRepository, ICacheProvider cacheProvider)
            : base(mediator, logger.CreateLogger<ObterTaxaCobrancaPorSegmentoHandler>())
        {
            _cacheProvider = cacheProvider;
            _taxaCobrancaRepository = taxaCobrancaRepository;
        }

        public async Task<ObterTaxaCobrancaPorSegmentoResponse> Handle(ObterTaxaCobrancaPorSegmentoQuery request, CancellationToken cancellationToken)
        {
            var response = (ObterTaxaCobrancaPorSegmentoResponse)request.Response;

            var tipoSegmento = TipoSegmento.ObterPorId(request.TipoSegmento);
            if (tipoSegmento is null)
            {
                response.AddError(Errors.General.NotFound("TipoSegmento", request.TipoSegmento));
                return response;
            }

            var tipoSegmentoResponse = await _cacheProvider.GetValueOrCreate(tipoSegmento.Id,
                                                                             async () => await ObterTaxaCobrancaPorSegmento(request, response, tipoSegmento));

            if (tipoSegmentoResponse.Equals(default(TaxaResponse)))
            {
                response.AddError(Errors.General.NotFound("Segmento", request.TipoSegmento));
                return response;
            }

            response.SetPayLoad(tipoSegmentoResponse);

            return response;
        }

        private async Task<TaxaResponse> ObterTaxaCobrancaPorSegmento(ObterTaxaCobrancaPorSegmentoQuery request, ObterTaxaCobrancaPorSegmentoResponse response, TipoSegmento tipoSegmento)
        {
            var taxaCobranca = await _taxaCobrancaRepository.ObterTaxaCobrancaPorSegmento(tipoSegmento.Id);
            if (string.IsNullOrEmpty(taxaCobranca.TaxaCobrancaId))
            {
                response.AddError(Errors.General.NotFound(nameof(TaxaCobranca), request.TipoSegmento));
                Logger.LogWarning($"{response.ErrorResponse}");

                return default;
            }

            return taxaCobranca.ConverterEntidadeParaResponse();
        }
    }
}