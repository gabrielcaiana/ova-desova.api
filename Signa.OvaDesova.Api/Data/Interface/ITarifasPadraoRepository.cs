using Signa.OvaDesova.Api.Domain.Models;

namespace Signa.OvaDesova.Api.Data.Interface
{
    interface ITarifasPadraoRepository
    {
        TarifasPadraoModel ConsultarTarifasPadrao(int tabelaOvaDesovaId);
    }
}