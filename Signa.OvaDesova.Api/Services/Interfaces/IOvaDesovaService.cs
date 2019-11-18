using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IOvaDesovaService
    {
        IEnumerable<OvaDesovaModel> Listar();
        OvaDesovaModel Consultar(int id);
        OvaDesovaModel Gravar(OvaDesovaModel Model);
        OvaDesovaModel Atualizar(OvaDesovaModel Model);
        void Excluir(int id);
    }
}
