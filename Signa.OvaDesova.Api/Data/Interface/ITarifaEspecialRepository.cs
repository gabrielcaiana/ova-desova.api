using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Interface
{
    interface ITarifaEspecialRepository
    {
        IEnumerable<TarifaEspecialModel> ConsultarTarifaEspecial(int tabelaPrecoFornecedorId);
        int Insert(TarifaEspecialModel tarifaEspecial);
        void Update(TarifaEspecialModel tarifaEspecial);
        void Delete(int tabelaTarifaEspecialId);
        bool VerificarDuplicidade(TarifaEspecialModel tarifaEspecial);
        void GravarHistorico(int tabelaTarifaEspecialId, int usuarioId);
    }
}