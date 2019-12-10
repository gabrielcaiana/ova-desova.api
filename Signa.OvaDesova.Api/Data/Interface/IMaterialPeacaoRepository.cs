using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Interface
{
    interface IMaterialPeacaoRepository
    {
        IEnumerable<MaterialPeacaoModel> ConsultarMaterialPeacao(int tabelaPrecoFornecedorId);
        int Insert(MaterialPeacaoModel materialPeacao);
        void Update(MaterialPeacaoModel materialPeacao);
        void Delete(int tabelaTarifaMaterialId);
        bool VerificarDuplicidade(MaterialPeacaoModel materialPeacao);
        void GravarHistorico(int tabelaTarifaMaterialId, int usuarioId);
        IEnumerable<MaterialPeacaoModel> ConsultarHistorico(int tabelaTarifaMaterialId);
        IEnumerable<MaterialPeacaoModel> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId);
    }
}