namespace Demo.Exchange.Infra.Repositories.Statements
{
    internal static class TaxaCobrancaStatements
    {
        private const string QueryDefault = @"
	        TaxaCobrancaId      TaxaCobrancaId
	        ,ValorTaxa          ValorTaxa
	        ,TipoSegmento       Segmento
	        ,CriadoEm           CriadoEm
            ,AtualizadoEm       AtualizadoEm";

        public static string ObterPorId = $"SELECT {QueryDefault} FROM {DemoExchangeRepositoriesConvetion.DefaultDataBase}.TaxaCobranca TaxaCobranca WHERE TaxaCobranca.TaxaCobrancaId = @id";

        public static string ObterTaxaCobrancaPorSegmento = $"SELECT {QueryDefault} FROM {DemoExchangeRepositoriesConvetion.DefaultDataBase}.TaxaCobranca TaxaCobranca WHERE TaxaCobranca.TipoSegmento = @segmento";

        public static string Registrar = $@"
        INSERT INTO {DemoExchangeRepositoriesConvetion.DefaultDataBase}.TaxaCobranca(
	        TaxaCobrancaId
	        ,ValorTaxa
	        ,TipoSegmento
	        ,CriadoEm
        )
        VALUES(
	        @TaxaCobrancaId
	        ,@ValorTaxa
	        ,@TipoSegmento
	        ,@CriadoEm
        )";

        public static string Atualizar = $@"
        UPDATE {DemoExchangeRepositoriesConvetion.DefaultDataBase}.TaxaCobranca SET
	        ValorTaxa = @ValorTaxa
	        ,AtualizadoEm = @AtualizadoEm
        WHERE TaxaCobrancaId = @TaxaCobrancaId";
    }
}