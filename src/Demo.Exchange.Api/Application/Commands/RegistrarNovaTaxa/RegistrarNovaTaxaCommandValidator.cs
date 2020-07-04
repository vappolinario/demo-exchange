namespace Demo.Exchange.Application.Commands.RegistrarNovaTaxa
{
    using FluentValidation;

    public class RegistrarNovaTaxaCommandValidator : AbstractValidator<RegistrarNovaTaxaCommand>
    {
        private RegistrarNovaTaxaCommandValidator()
        {
            RuleFor(x => x.Segmento)
                .NotEmpty()
                .WithErrorCode("TipoSegmentoInvalido")
                .WithMessage("Segemento não contém um valo válido entro Varejo, Personnalite, Private");

            RuleFor(x => x.ValorTaxa)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("ValorTaxaInvalido")
                .WithMessage("Valor Taxa não deve ser menor que zero.");
        }

        public static void ValidarCommand(RegistrarNovaTaxaCommand request, RegistrarNovaTaxaResponse response)
        {
            var validator = new RegistrarNovaTaxaCommandValidator();

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