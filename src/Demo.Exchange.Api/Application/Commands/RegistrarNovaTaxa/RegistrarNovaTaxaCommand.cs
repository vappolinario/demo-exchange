namespace Demo.Exchange.Application.Commands.RegistrarNovaTaxa
{
    using MediatR;

    public class RegistrarNovaTaxaCommand : Request, IRequest<RegistrarNovaTaxaResponse>
    {
        protected RegistrarNovaTaxaCommand()
        {
        }

        public RegistrarNovaTaxaCommand(string segmento, decimal valorTaxa)
        {
            Segmento = segmento;
            ValorTaxa = valorTaxa;
        }

        public string Segmento { get; set; }
        public decimal ValorTaxa { get; set; }

        public override Response Response => new RegistrarNovaTaxaResponse(RequestId);
    }
}