using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface ITarifasPadraoService
    {
        IEnumerable<TarifasPadraoModel> ConsultarTarifasPadrao(int tabelaPrecoFornecedorId);
        int Save(TarifasPadraoModel tarifasPadrao);
        void Delete(int tabelaOvaDesovaId);
    }
}