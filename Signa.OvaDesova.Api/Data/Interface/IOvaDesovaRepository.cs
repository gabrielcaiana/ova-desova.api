using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Interface
{    
    interface IOvaDesovaRepository
    {
        IEnumerable<ResultadoModel> GetAll(ConsultaModel consulta);
    }
}