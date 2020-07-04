using MediatR;

namespace Demo.Exchange.Domain.AggregateModel.TaxaModel
{
    public class NovaTaxaRegistradaEvent : INotification
    {
        public NovaTaxaRegistradaEvent(TaxaCobranca taxaCobranca)
        {
            TaxaCobranca = taxaCobranca;
        }

        public TaxaCobranca TaxaCobranca { get; }
    }
}