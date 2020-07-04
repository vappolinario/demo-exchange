namespace Demo.Exchange.Application.Commands.AtualizarTaxa
{
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using Demo.Exchange.Infra.Extensions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class AtualizarTaxaCommandHandler : Handler, IRequestHandler<AtualizarTaxaCommand, AtualizarTaxaResponse>
    {
        private TaxaCobranca TaxaCobranca;
        private ValorTaxaCobranca ValorTaxaCobranca;

        private readonly ITaxaCobrancaRepository _taxaCobrancaRepository;

        public AtualizarTaxaCommandHandler(IMediator mediator, ILoggerFactory logger, ITaxaCobrancaRepository taxaCobrancaRepository)
            : base(mediator, logger.CreateLogger<AtualizarTaxaCommandHandler>())
        {
            _taxaCobrancaRepository = taxaCobrancaRepository;
        }

        public async Task<AtualizarTaxaResponse> Handle(AtualizarTaxaCommand request, CancellationToken cancellationToken)
        {
            var response = (AtualizarTaxaResponse)request.Response;

            AtualizarTaxaCommandValidator.ValidarCommand(request, response);
            if (response.IsFailure)
                return response;

            await ObterTaxaPorId(request, response);
            if (response.IsFailure)
                return response;

            CriarNovoValorTaxa(request, response);
            if (response.IsFailure)
                return response;

            var atualizarTaxaResultado = TaxaCobranca.AtualizarTaxa(ValorTaxaCobranca);
            if (atualizarTaxaResultado.IsFailure)
            {
                response.AddError(Errors.General
                                        .InvalidCommandArguments()
                                        .AddErroDetail(Errors.AtualizarTaxaErros.ValorTaxaSemAlteracao(TaxaCobranca.ValorTaxa.Valor, request.NovaTaxa)));
                return response;
            }

            await AtualizarTaxaCobranca(request, response);
            if (response.IsFailure)
                return response;

            await Mediator.DispatchDomainEvents(TaxaCobranca);

            return response;
        }

        private void CriarNovoValorTaxa(AtualizarTaxaCommand request, AtualizarTaxaResponse response)
        {
            var novoValorCobranca = ValorTaxaCobranca.Create(request.NovaTaxa);
            if (novoValorCobranca.IsFailure)
            {
                response.AddError(Errors.General
                                        .InvalidCommandArguments()
                                        .AddErroDetail(Errors.General.InvalidArgument("ValorNovaTaxaInvalido", string.Join("|", novoValorCobranca.Messages))));

                Logger.LogWarning($"{response.ErrorResponse}");
                return;
            }

            ValorTaxaCobranca = novoValorCobranca.Value;
        }

        private async Task ObterTaxaPorId(AtualizarTaxaCommand request, AtualizarTaxaResponse response)
        {
            try
            {
                TaxaCobranca = await _taxaCobrancaRepository.ObterPorId(request.Id);
                if (string.IsNullOrEmpty(TaxaCobranca.TaxaCobrancaId))
                {
                    response.AddError(Errors.General.NotFound(nameof(TaxaCobranca), request.Id));

                    Logger.LogWarning($"{response.ErrorResponse}");
                    return;
                }
            }
            catch (Exception ex)
            {
                response.AddError(Errors.General.InternalProcessError("ObterTaxaPorId", ex.Message));

                Logger.LogError(ex, $"{response.ErrorResponse}");
                return;
            }
        }

        private async Task AtualizarTaxaCobranca(AtualizarTaxaCommand request, AtualizarTaxaResponse response)
        {
            try
            {
                await _taxaCobrancaRepository.Atualizar(TaxaCobranca);
            }
            catch (Exception ex)
            {
                response.AddError(Errors.General.InternalProcessError("AtualizarTaxaCobranca", ex.Message));
                Logger.LogError(ex, $"{response.ErrorResponse}");

                return;
            }
        }
    }
}