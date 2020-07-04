namespace Demo.Exchange.Application.Commands.AtualizarTaxa
{
    using FluentValidation;

    public class AtualizarTaxaCommandValidator : AbstractValidator<AtualizarTaxaCommand>
    {
        private AtualizarTaxaCommandValidator()
        {
            RuleFor(x => x.NovaTaxa)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("NovaTaxaInvalido")
                .WithMessage("Valor Nova Taxa não deve ser menor que zero.");
        }

        public static void ValidarCommand(AtualizarTaxaCommand request, AtualizarTaxaResponse response)
        {
            var validator = new AtualizarTaxaCommandValidator();

            var resultadoValidacao = validator.Validate(request);
            if (!resultadoValidacao.IsValid)
            {
                var invalidCommandArguments = Errors.General.InvalidCommandArguments();

                foreach (var erro in resultadoValidacao.Errors)
                {
                    invalidCommandArguments.AddErroDetail(Errors.General.InvalidArgument(erro.ErrorCode, erro.ErrorMessage));
                }

                response.AddError(invalidCommandArguments);
            }
        }
    }
}