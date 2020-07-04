namespace Demo.Exchange.Application.Queries.ObterCotacaoPorMoeda
{
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using Demo.Exchange.Domain.Services.CadernoFomulas;
    using Demo.Exchange.Infra.Connectors;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ObterCotacaoPorMoedaHandler : Handler, IRequestHandler<ObterCotacaoPorMoedaQuery, ObterCotacaoPorMoedaResponse>
    {
        private TaxaCobranca TaxaCobranca;
        private ParametroOutput ValorConversao;
        private KeyValuePair<string, decimal> Cotacao;

        private readonly ICadernoFormulasService _cadernoFormulasService;
        private readonly ITaxaCobrancaRepository _taxaCobrancaRepository;
        private readonly IExchangeRatesApiConnector _exchangeRatesApiConnector;

        public ObterCotacaoPorMoedaHandler(IMediator mediator,
                                           ILoggerFactory logger,
                                           IExchangeRatesApiConnector exchangeRatesApiConnector,
                                           ICadernoFormulasService cadernoFormulasService,
                                           ITaxaCobrancaRepository taxaCobrancaRepository)
            : base(mediator, logger.CreateLogger<ObterCotacaoPorMoedaHandler>())
        {
            _cadernoFormulasService = cadernoFormulasService;
            _taxaCobrancaRepository = taxaCobrancaRepository;
            _exchangeRatesApiConnector = exchangeRatesApiConnector;
        }

        public async Task<ObterCotacaoPorMoedaResponse> Handle(ObterCotacaoPorMoedaQuery request, CancellationToken cancellationToken)
        {
            var response = (ObterCotacaoPorMoedaResponse)request.Response;

            var tipoSegmento = TipoSegmento.ObterPorId(request.Segmento);
            if (tipoSegmento is null)
            {
                response.AddError(Errors.General.NotFound(nameof(TipoSegmento), request.Segmento));

                Logger.LogWarning($"{response.ErrorResponse}");
                return response;
            }

            await ObterTaxaCobrancaPorSegmento(request, response, tipoSegmento);
            if (response.IsFailure)
                return response;

            Cotacao = await _exchangeRatesApiConnector.OberUltimaCotacaoPorMoeda(request.Moeda);

            ExecutarCalculoContacao(request, response);
            if (response.IsFailure)
                return response;

            response.SetPayLoad(CreateResponse(request));

            return response;
        }

        private CotacaoPorMoedaResponse CreateResponse(ObterCotacaoPorMoedaQuery request)
        {
            return new CotacaoPorMoedaResponse
            {
                MoedaDe = request.Moeda,
                MoedaPara = Cotacao.Key,
                ValorCotacao = ValorConversao.Valor,
                Quantidade = request.Quantidade
            };
        }

        private async Task ObterTaxaCobrancaPorSegmento(ObterCotacaoPorMoedaQuery request, ObterCotacaoPorMoedaResponse response, TipoSegmento tipoSegmento)
        {
            try
            {
                TaxaCobranca = await _taxaCobrancaRepository.ObterTaxaCobrancaPorSegmento(tipoSegmento.Id);
                if (string.IsNullOrEmpty(TaxaCobranca.TaxaCobrancaId))
                {
                    response.AddError(Errors.General.NotFound(nameof(TaxaCobranca), request.Segmento));

                    Logger.LogWarning($"{response.ErrorResponse}");
                    return;
                }
            }
            catch (Exception ex)
            {
                response.AddError(Errors.General.InternalProcessError("ObterTaxaCobrancaPorSegmento", ex.Message));

                Logger.LogError(ex, $"{response.ErrorResponse}");
                return;
            }
        }

        private void ExecutarCalculoContacao(ObterCotacaoPorMoedaQuery request, ObterCotacaoPorMoedaResponse response)
        {
            var formula = new ConversaoMoedaFormula(new ConversaoMoedaParametroInput(request.Quantidade, Cotacao.Value, TaxaCobranca.ValorTaxa.Valor));

            var resultadoValorContacao = _cadernoFormulasService.Compute(formula);
            if (resultadoValorContacao.IsFailure)
            {
                var countError = 1;
                var invalidQueryParameters = Errors.General.InvalidQueryParameters();
                foreach (var error in resultadoValorContacao.Messages)
                    invalidQueryParameters.AddErroDetail(Errors.General.InvalidArgument($"InvalidArgumentConsulta{countError}", error));

                response.AddError(invalidQueryParameters);
            }

            ValorConversao = resultadoValorContacao.Value;
        }
    }
}