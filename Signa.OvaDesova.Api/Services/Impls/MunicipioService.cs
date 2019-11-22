using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Impls
{
    class MunicipioService : IMunicipioService
    {
        IMunicipioRepository _municipio;

        public MunicipioService()
        {
            _municipio = new MunicipioRepository();
        }

        public IEnumerable<MunicipioModel> GetAll()
        {
            return _municipio.GetAll();
        }

        public IEnumerable<UfModel> GetAllUf()
        {
            return _municipio.GetAllUf();
        }
    }
}