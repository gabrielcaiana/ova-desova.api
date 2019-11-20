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
    }
}