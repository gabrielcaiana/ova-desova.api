using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IOvaDesovaService
    {
        IEnumerable<TabelaPrecoFornecedorModel> GetAll(ConsultaModel consulta);
    }
}