using Signa.OvaDesova.Api.Domain.Models;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface ITarifasPadraoService
    {
        TarifasPadraoModel ConsultarTarifasPadrao(int tabelaOvaDesovaId);
    }
}