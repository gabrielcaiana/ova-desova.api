using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class MunicipioRepository : RepositoryBase, IMunicipioRepository
    {
        public IEnumerable<MunicipioModel> GetAll()
        {
            var sql = @"Select 
                               Municipio_Id MunicipioId,
                               Municipio + ' - ' + Uf NomeMunicipio
                        From   
                             Municipio
                        Where  Tab_Status_Id = 1
                        Order By 
                                 Municipio";

            return RepositoryHelper.Query<MunicipioModel>(sql, null, CommandType.Text);
        }

        public IEnumerable<UfModel> GetAllUf()
        {
            var sql = @"
                        SELECT
	                        TAB_UF_ID UfId,
	                        NOME_UF NomeUf
                        FROM
	                        TAB_UF
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TAB_UF_ID NOT IN (28, 29)
                        ORDER BY
	                        NOME_UF";

            return RepositoryHelper.Query<UfModel>(sql, null, CommandType.Text);
        }
    }
}