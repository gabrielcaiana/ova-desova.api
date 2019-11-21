﻿using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class OvaDesovaRepository : RepositoryBase, IOvaDesovaRepository
    {
        public IEnumerable<TabelaPrecoFornecedorModel> GetAll(ConsultaModel consulta)
        {
            var sql = @"
                        SELECT DISTINCT
	                        TPF.TABELA_PRECO_FORNECEDOR_ID				TabelaPrecoFornecedorId,
                            F.FORNECEDOR_ID								FornecedorId,
	                        F.NOME_FANTASIA								NomeFantasia,
                            F.CNPJ_CPF									Cnpj,
	                        F.IE_RG										InscricaoEstadual,
                            TOD.MUNICIPIO_ID							Municipio_Id,
                            F.MUNICIPIO + ' - ' + F.UF					Municipio,
	                        CONVERT(VARCHAR(10), TPF.DATA_INICIO, 103)	DataInicio,
	                        CONVERT(VARCHAR(10), TPF.DATA_FIM, 103)		DataFim
                        FROM
	                        TABELA_PRECO_FORNECEDOR TPF
	                        INNER JOIN TABELA_OVA_DESOVA TOD ON TOD.TABELA_PRECO_FORNECEDOR_ID = TPF.TABELA_PRECO_FORNECEDOR_ID
	                        OUTER APPLY (SELECT FORNECEDOR_ID, NOME_FANTASIA, CNPJ_CPF, IE_RG, MUNICIPIO, UF FROM VFORNEC_TAB_TIPO_FORNEC2 WHERE PESSOA_ID = TPF.FORNECEDOR_ID) F
                        WHERE
	                        TPF.TAB_STATUS_ID = 1
	                        AND TOD.TAB_STATUS_ID = 1
	                        AND TPF.TAB_TIPO_TABELA_ID = 50
	                        AND (TOD.MUNICIPIO_ID = @MunicipioId OR @MunicipioId = 0)
	                        AND (TPF.FORNECEDOR_ID = @FornecedorId OR @FornecedorId = 0)
	                        AND	(TPF.DATA_INICIO >= @PeriodoDe OR @PeriodoDe = '')
	                        AND	(TPF.DATA_FIM <= @PeriodoAte OR @PeriodoAte = '')
                        ORDER BY F.NOME_FANTASIA";

            var param = new
            {
                consulta.MunicipioId,
                consulta.FornecedorId,
                consulta.PeriodoDe,
                consulta.PeriodoAte
            };

            return RepositoryHelper.Query<TabelaPrecoFornecedorModel>(sql, param, CommandType.Text);
        }
    }
}