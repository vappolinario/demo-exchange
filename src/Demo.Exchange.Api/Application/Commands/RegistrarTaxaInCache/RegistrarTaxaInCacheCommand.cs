namespace Demo.Exchange.Application.Commands.RegistrarTaxaInCache
{
    using MediatR;

    public class RegistrarTaxaInCacheCommand : Request, IRequest<RegistrarTaxaInCacheResponse>
    {
        public RegistrarTaxaInCacheCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
        public override Response Response => new RegistrarTaxaInCacheResponse(RequestId);
    }
}