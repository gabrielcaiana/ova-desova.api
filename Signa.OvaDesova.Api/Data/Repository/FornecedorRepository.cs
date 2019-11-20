using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class FornecedorRepository : RepositoryBase, IFornecedorRepository
    {
        public IEnumerable<FornecedorModel> GetAll()
        {
            var sql = @"
                        SELECT
                            FORNECEDOR_ID FornecedorId,
	                        NOME_FANTASIA NomeFantasia,
                            CNPJ_CPF Cnpj,
	                        IE_RG InscricaoEstadual,
                            MUNICIPIO + ' - ' + UF Municipio
                        FROM VFORNEC_TAB_TIPO_FORNEC2
                        WHERE TAB_STATUS_ID = 1
                        ORDER BY NOME_FANTASIA";

            return RepositoryHelper.Query<FornecedorModel>(sql, null, CommandType.Text);
        }
    }
}