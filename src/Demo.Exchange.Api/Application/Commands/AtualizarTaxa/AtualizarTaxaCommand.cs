namespace Demo.Exchange.Application.Commands.AtualizarTaxa
{
    using MediatR;

    public class AtualizarTaxaCommand : Request, IRequest<AtualizarTaxaResponse>
    {
        public string Id { get; private set; }
        public decimal NovaTaxa { get; set; }

        public void SetId(string id) => Id = id;

        public override Response Response => new AtualizarTaxaResponse(RequestId);
    }
}