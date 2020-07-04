using System.Threading.Tasks;

namespace Demo.Exchange.Domain.AggregateModel.TaxaModel
{
    public interface ITaxaCobrancaRepository
    {
        Task Registrar(TaxaCobranca taxaCobranca);

        Task<TaxaCobranca> ObterPorId(string id);

        Task<TaxaCobranca> ObterTaxaCobrancaPorSegmento(string segmento);

        Task Atualizar(TaxaCobranca taxaCobranca);
    }
}