using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class OvaDesovaRepository : RepositoryBase, IOvaDesovaRepository
    {
        public IEnumerable<ResultadoModel> GetAll(ConsultaModel consulta)
        {
            var sql = @"
                        SELECT DISTINCT
	                        TPF.TABELA_PRECO_FORNECEDOR_ID							TabelaPrecoFornecedorId,
	                        ISNULL(F.NOME_FANTASIA, '')								NomeFantasia,
	                        ISNULL(F.CNPJ_CPF, '')									Cnpj,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_INICIO, 103), '')  DataInicio,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_FIM, 103), '')     DataFim
                        FROM
	                        TABELA_PRECO_FORNECEDOR TPF
	                        LEFT JOIN TABELA_OVA_DESOVA TOD ON TOD.TABELA_PRECO_FORNECEDOR_ID = TPF.TABELA_PRECO_FORNECEDOR_ID
	                            AND TOD.TAB_STATUS_ID = 1
	                        LEFT JOIN MUNICIPIO M ON M.MUNICIPIO_ID = TOD.MUNICIPIO_ID
	                        OUTER APPLY (SELECT NOME_FANTASIA, CNPJ_CPF FROM VFORNEC_TAB_TIPO_FORNEC2 WHERE PESSOA_ID = TPF.FORNECEDOR_ID) F
                        WHERE
	                        TPF.TAB_STATUS_ID IN (1, 2)
	                        AND TPF.TAB_TIPO_TABELA_ID = 50
	                        AND (TOD.MUNICIPIO_ID = @MunicipioId OR ISNULL(@MunicipioId, 0) = 0)
	                        AND (M.TAB_UF_ID = @UfId OR ISNULL(@UfId, 0) = 0)
							AND (
									(ISNULL(TPF.FORNECEDOR_ID, 0) = 0 AND @TipoTabela = 'TP') OR
									(ISNULL(TPF.FORNECEDOR_ID, 0) <> 0 AND (TPF.FORNECEDOR_ID = @FornecedorId OR ISNULL(@FornecedorId, 0) = 0) AND @TipoTabela = 'P') OR
									(TPF.FORNECEDOR_ID = @FornecedorId OR ISNULL(@FornecedorId, 0) = 0 AND ISNULL(@TipoTabela, '') = '')
								)
	                        AND (
			                        (TPF.DATA_INICIO BETWEEN @PeriodoDe AND @PeriodoAte) OR
			                        (TPF.DATA_FIM BETWEEN @PeriodoDe AND @PeriodoAte) OR
			                        (ISNULL(@PeriodoDe, '') = '' AND ISNULL(@PeriodoAte, '') = '')
		                        )
                        ORDER BY NomeFantasia";

            var param = new
            {
                consulta.MunicipioId,
                consulta.UfId,
                consulta.FornecedorId,
                consulta.PeriodoDe,
                consulta.PeriodoAte,
                consulta.TipoTabela
            };

            return RepositoryHelper.Query<ResultadoModel>(sql, param, CommandType.Text);
        }
    }
}