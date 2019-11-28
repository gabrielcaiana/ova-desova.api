using Signa.OvaDesova.Api.Domain.Models;

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
    }
}