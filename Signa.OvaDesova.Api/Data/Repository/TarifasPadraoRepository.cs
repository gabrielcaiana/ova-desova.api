using Dapper;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
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
    }
}