namespace Demo.Exchange.Application.Commands.RegistrarTaxaInCache
{
    using Demo.Exchange.Application.Commands.RegistrarNovaTaxa;
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using Demo.Exchange.Infra.Cache.Memcached;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    public class RegistrarTaxaInCacheCommandHandler : Handler, IRequestHandler<RegistrarTaxaInCacheCommand, RegistrarTaxaInCacheResponse>
    {
        private TaxaCobranca TaxaCobranca;

        private readonly ICacheRepository _cacheRepository;
        private readonly ITaxaCobrancaRepository _taxaCobrancaRepository;

        public RegistrarTaxaInCacheCommandHandler(IMediator mediator,
                                                  ILoggerFactory logger,
                                                  ITaxaCobrancaRepository taxaCobrancaRepository,
                                                  ICacheRepository cacheRepository)
            : base(mediator, logger.CreateLogger<RegistrarTaxaInCacheCommandHandler>())
        {
            _cacheRepository = cacheRepository;
            _taxaCobrancaRepository = taxaCobrancaRepository;
        }

        public async Task<RegistrarTaxaInCacheResponse> Handle(RegistrarTaxaInCacheCommand request, CancellationToken cancellationToken)
        {
            var response = (RegistrarTaxaInCacheResponse)request.Response;

            TaxaCobranca = await _taxaCobrancaRepository.ObterPorId(request.Id);

            var taxaResponse = TaxaCobranca.ConverterEntidadeParaResponse();

            await _cacheRepository.Set(taxaResponse.Id, taxaResponse);
            await _cacheRepository.Set(taxaResponse.TipoSegmento, taxaResponse);

            return response;
        }
    }
}