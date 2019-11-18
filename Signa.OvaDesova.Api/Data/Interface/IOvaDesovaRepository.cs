using Signa.OvaDesova.Api.Domain.Entities;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Interface
{    
    interface IOvaDesovaRepository
    {
        IEnumerable<OvaDesovaEntity> GetAll();
        OvaDesovaEntity GetById(int pessoaId);
        dynamic Delete(int pessoaId);
        dynamic Update(OvaDesovaEntity pessoa);
        int Insert(OvaDesovaEntity pessoa);
    }
}
