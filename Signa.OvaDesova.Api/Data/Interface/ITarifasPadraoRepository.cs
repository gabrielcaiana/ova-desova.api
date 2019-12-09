using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Interface
{
    interface ITarifasPadraoRepository
    {
        IEnumerable<TarifasPadraoModel> ConsultarTarifasPadrao(int tabelaPrecoFornecedorId);
        int Insert(TarifasPadraoModel tarifasPadrao);
        void Update(TarifasPadraoModel tarifasPadrao);
        void Delete(int tabelaOvaDesovaId);
        bool VerificarDuplicidade(TarifasPadraoModel tarifasPadrao);
        void GravarHistorico(int tabelaOvaDesovaId, int usuarioId);
        IEnumerable<TarifasPadraoModel> ConsultarHistorico(int tabelaOvaDesovaId);
    }
}