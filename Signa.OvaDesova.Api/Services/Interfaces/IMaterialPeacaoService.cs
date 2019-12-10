using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IMaterialPeacaoService
    {
        IEnumerable<MaterialPeacaoModel> ConsultarMaterialPeacao(int tabelaPrecoFornecedorId);
        int Save(MaterialPeacaoModel MaterialPeacao);
        void Delete(int tabelaTarifaMaterialId);
        IEnumerable<MaterialPeacaoModel> ConsultarHistorico(int tabelaTarifaMaterialId);
        IEnumerable<MaterialPeacaoModel> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId);
    }
}