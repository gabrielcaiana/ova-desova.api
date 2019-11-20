using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IMunicipioService
    {
        IEnumerable<MunicipioModel> GetAll();
    }
}