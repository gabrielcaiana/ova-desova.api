using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface ITarifaEspecialService
    {
        IEnumerable<TarifaEspecialModel> ConsultarTarifaEspecial(int tabelaPrecoFornecedorId);
        int Save(TarifaEspecialModel tarifaEspecial);
        void Delete(int tabelaTarifaEspecialId);
    }
}