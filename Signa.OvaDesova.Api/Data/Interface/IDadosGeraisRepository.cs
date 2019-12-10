using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Interface
{
    interface IDadosGeraisRepository
    {
        DadosGeraisModel ConsultarDadosGerais(int tabelaPrecoFornecedorId);
        int Insert(DadosGeraisModel dadosGerais);
        void Update(DadosGeraisModel dadosGerais);
        void Delete(int tabelaPrecoFornecedorId);
        bool VerificarDuplicidade(DadosGeraisModel dadosGerais);
        void GravarHistorico(int tabelaPrecoFornecedorId, int usuarioId);
        IEnumerable<DadosGeraisModel> ConsultarHistorico(int tabelaPrecoFornecedorId);
    }
}