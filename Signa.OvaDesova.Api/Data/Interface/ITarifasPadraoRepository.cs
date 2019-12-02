using Signa.OvaDesova.Api.Domain.Models;

namespace Signa.OvaDesova.Api.Data.Interface
{
    interface ITarifasPadraoRepository
    {
        TarifasPadraoModel ConsultarTarifasPadrao(int tabelaOvaDesovaId);
        int Insert(TarifasPadraoModel tarifasPadrao);
        void Update(TarifasPadraoModel tarifasPadrao);
        void Delete(int tabelaOvaDesovaId);
        bool VerificarDuplicidade(TarifasPadraoModel tarifasPadrao);
        void GravarHistorico(int tabelaOvaDesovaId, int usuarioId);
    }
}