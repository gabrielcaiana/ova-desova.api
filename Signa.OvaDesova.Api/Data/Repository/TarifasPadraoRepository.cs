using Dapper;
using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Data;
using System.Linq;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class TarifasPadraoRepository : RepositoryBase, ITarifasPadraoRepository
    {
        public TarifasPadraoModel ConsultarTarifasPadrao(int tabelaOvaDesovaId)
        {
            var sql = @"
                        SELECT
	                        TOD.TABELA_OVA_DESOVA_ID TabelaOvaDesovaId,
	                        TOD.TABELA_PRECO_FORNECEDOR_ID TabelaPrecoFornecedorId,
	                        TOD.VAL_CONFERENTE Conferente,
	                        TOD.AJUDANTE_1 Ajudante1,
	                        TOD.AJUDANTE_2 Ajudante2,
	                        TOD.AJUDANTE_3 Ajudante3,
	                        TOD.AJUDANTE_4 Ajudante4,
	                        TOD.AJUDANTE_5 Ajudante5,
	                        TOD.AJUDANTE_6 Ajudante6,
	                        TOD.AJUDANTE_7 Ajudante7,
	                        TOD.AJUDANTE_8 Ajudante8,
	                        TOD.MUNICIPIO_ID MunicipioId,
	                        MUN.MUNICIPIO NomeMunicipio
                        FROM
	                        TABELA_OVA_DESOVA TOD
	                        INNER JOIN MUNICIPIO MUN ON MUN.MUNICIPIO_ID = TOD.MUNICIPIO_ID
                        WHERE
	                        TOD.TABELA_OVA_DESOVA_ID = @TabelaOvaDesovaId";

            var param = new
            {
                TabelaOvaDesovaId = tabelaOvaDesovaId
            };

            using (var db = Connection)
            {
                return db.Query<TarifasPadraoModel, MunicipioModel, TarifasPadraoModel>(
                        sql,
                        (tarifasPadraoModel, municipioModel) =>
                        {
                            tarifasPadraoModel.Municipio = municipioModel;
                            return tarifasPadraoModel;
                        },
                        param,
                        splitOn: "MunicipioId"
                        ).FirstOrDefault();
            }
        }

        public int Insert(TarifasPadraoModel tarifasPadrao)
        {
            var sql = @"
                        INSERT INTO TABELA_OVA_DESOVA
                        (
	                        TABELA_PRECO_FORNECEDOR_ID,
	                        VAL_CONFERENTE,
	                        AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8,
	                        MUNICIPIO_ID,
	                        TAB_STATUS_ID
                        )
                        VALUES
                        (
                            @TabelaPrecoFornecedorId,
                            @Conferente,
                            @Ajudante1,
                            @Ajudante2,
                            @Ajudante3,
                            @Ajudante4,
                            @Ajudante5,
                            @Ajudante6,
                            @Ajudante7,
                            @Ajudante8,
                            @MunicipioId,
                            1
                        )

                        SELECT SCOPE_IDENTITY()";

            var param = new
            {
                tarifasPadrao.TabelaPrecoFornecedorId,
                tarifasPadrao.Conferente,
                tarifasPadrao.Ajudante1,
                tarifasPadrao.Ajudante2,
                tarifasPadrao.Ajudante3,
                tarifasPadrao.Ajudante4,
                tarifasPadrao.Ajudante5,
                tarifasPadrao.Ajudante6,
                tarifasPadrao.Ajudante7,
                tarifasPadrao.Ajudante8,
                tarifasPadrao.Municipio.MunicipioId,
            };

            return RepositoryHelper.QueryFirstOrDefault<int>(sql, param, CommandType.Text);
        }

        public void Update(TarifasPadraoModel tarifasPadrao)
        {
            var sql = @"
                        UPDATE
	                        TABELA_OVA_DESOVA
                        SET
	                        VAL_CONFERENTE = Conferente,
	                        AJUDANTE_1 = Ajudante1,
	                        AJUDANTE_2 = Ajudante2,
	                        AJUDANTE_3 = Ajudante3,
	                        AJUDANTE_4 = Ajudante4,
	                        AJUDANTE_5 = Ajudante5,
	                        AJUDANTE_6 = Ajudante6,
	                        AJUDANTE_7 = Ajudante7,
	                        AJUDANTE_8 = Ajudante8,
	                        MUNICIPIO_ID = MunicipioId
                        WHERE
	                        TABELA_OVA_DESOVA_ID = @TabelaOvaDesovaId";

            var param = new
            {
                tarifasPadrao.TabelaOvaDesovaId,
                tarifasPadrao.Conferente,
                tarifasPadrao.Ajudante1,
                tarifasPadrao.Ajudante2,
                tarifasPadrao.Ajudante3,
                tarifasPadrao.Ajudante4,
                tarifasPadrao.Ajudante5,
                tarifasPadrao.Ajudante6,
                tarifasPadrao.Ajudante7,
                tarifasPadrao.Ajudante8,
                tarifasPadrao.Municipio.MunicipioId,
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }

        public void Delete(int tabelaOvaDesovaId)
        {
            var sql = @"
                        UPDATE
	                        TABELA_OVA_DESOVA
                        SET
	                        TAB_STATUS_ID = 2
                        WHERE
	                        TABELA_OVA_DESOVA_ID = @TabelaOvaDesovaId";

            var param = new
            {
                TabelaOvaDesovaId = tabelaOvaDesovaId
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }

        public bool VerificarDuplicidade(TarifasPadraoModel tarifasPadrao)
        {
            var sql = @"
                        SELECT
	                        1
                        FROM
	                        TABELA_OVA_DESOVA
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TABELA_OVA_DESOVA_ID <> @TabelaOvaDesovaId
	                        AND TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
	                        AND MUNICIPIO_ID = @MunicipioId";

            var param = new
            {
                tarifasPadrao.TabelaOvaDesovaId,
                tarifasPadrao.TabelaPrecoFornecedorId,
                tarifasPadrao.Municipio.MunicipioId
            };

            return RepositoryHelper.QueryFirstOrDefault<int>(sql, param, CommandType.Text) == 1;
        }

        public void GravarHistorico(int tabelaOvaDesovaId, int usuarioId)
        {
            var sql = @"
                        INSERT INTO HIST_TABELA_OVA_DESOVA
                        (
	                        TABELA_OVA_DESOVA_ID,
	                        TABELA_PRECO_FORNECEDOR_ID,
	                        MUNICIPIO_ID,
	                        VAL_CONFERENTE,
	                        AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8,
	                        TAB_STATUS_ID,
	                        DATA_INCL,
	                        USUARIO_INCL_ID
                        )
                        SELECT
	                        TABELA_OVA_DESOVA_ID,
	                        TABELA_PRECO_FORNECEDOR_ID,
	                        MUNICIPIO_ID,
	                        VAL_CONFERENTE,
	                        AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8,
	                        TAB_STATUS_ID,
	                        GETDATE(),
	                        @UsuarioId
                        FROM
	                        TABELA_OVA_DESOVA
                        WHERE
	                        TABELA_OVA_DESOVA_ID = @TabelaOvaDesovaId";

            var param = new
            {
                TabelaOvaDesovaId = tabelaOvaDesovaId,
                UsuarioId = usuarioId
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }
    }
}