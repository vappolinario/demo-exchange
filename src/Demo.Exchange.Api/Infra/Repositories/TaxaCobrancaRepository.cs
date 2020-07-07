namespace Demo.Exchange.Infra.Repositories
{
    using Dapper;
    using Demo.Exchange.Domain.AggregateModel.TaxaModel;
    using Demo.Exchange.Infra.Options;
    using Demo.Exchange.Infra.Repositories.Statements;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;

    public class TaxaCobrancaRepository : Repository, ITaxaCobrancaRepository
    {
        public TaxaCobrancaRepository(ILoggerFactory logger, IOptions<ConnectionStringOptions> connectionString)
            : base(logger.CreateLogger<TaxaCobrancaRepository>(), connectionString)
        {
        }

        public async Task Registrar(TaxaCobranca taxaCobranca) => await ExecutarRegistrar(taxaCobranca);

        public async Task Atualizar(TaxaCobranca taxaCobranca) => await ExecutarAtualizar(taxaCobranca);

        public async Task<TaxaCobranca> ObterPorId(string id) => await ExecutarObterPorId(id);

        public async Task<TaxaCobranca> ObterTaxaCobrancaPorSegmento(string segmento) => await ExecutarObterTaxaCobrancaPorSegmento(segmento);

        private async Task ExecutarRegistrar(TaxaCobranca taxaCobranca)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    await conn.ExecuteAsync(TaxaCobrancaStatements.Registrar,
                        new
                        {
                            taxaCobranca.TaxaCobrancaId,
                            ValorTaxa = taxaCobranca.ValorTaxa.Valor,
                            TipoSegmento = taxaCobranca.TipoSegmento.Id,
                            taxaCobranca.CriadoEm
                        });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao registrar a taxa de cobrança.");
                throw;
            }
        }

        private async Task ExecutarAtualizar(TaxaCobranca taxaCobranca)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    await conn.ExecuteAsync(TaxaCobrancaStatements.Atualizar,
                        new
                        {
                            taxaCobranca.TaxaCobrancaId,
                            ValorTaxa = taxaCobranca.ValorTaxa.Valor,
                            taxaCobranca.AtualizadoEm
                        });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao atualizar a taxa de cobrança.");
                throw;
            }
        }

        private async Task<TaxaCobranca> ExecutarObterTaxaCobrancaPorSegmento(string segmento)
        {
            var taxaCobranca = TaxaCobranca.EntidadeDefault();

            try
            {
                using (var conn = GetConnection())
                {
                    taxaCobranca = VerificarStatusConsulta(taxaCobranca, await conn.QueryFirstOrDefaultAsync<TaxaCobrancaDto>(TaxaCobrancaStatements.ObterTaxaCobrancaPorSegmento, new { segmento }));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao consultar a taxa de cobrança pelo tipo de segmento: {segmento}");
                throw;
            }

            return taxaCobranca;
        }

        private async Task<TaxaCobranca> ExecutarObterPorId(string id)
        {
            var taxaCobranca = TaxaCobranca.EntidadeDefault();

            try
            {
                using (var conn = GetConnection())
                {
                    taxaCobranca = VerificarStatusConsulta(taxaCobranca, await conn.QueryFirstOrDefaultAsync<TaxaCobrancaDto>(TaxaCobrancaStatements.ObterPorId, new { id }));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao consultar a taxa de cobrança pelo id: {id}");
                throw;
            }

            return taxaCobranca;
        }

        private static TaxaCobranca VerificarStatusConsulta(TaxaCobranca taxaCobranca, TaxaCobrancaDto dto)
        {
            if (!dto.Equals(default(TaxaCobrancaDto)))
                taxaCobranca = dto.ConverterDtoParaEntidade();

            return taxaCobranca;
        }
    }

    internal struct TaxaCobrancaDto
    {
        public string TaxaCobrancaId { get; set; }
        public decimal ValorTaxa { get; set; }
        public string Segmento { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        public TaxaCobranca ConverterDtoParaEntidade() => new TaxaCobranca(TaxaCobrancaId, ValorTaxa, Segmento);
    }
}