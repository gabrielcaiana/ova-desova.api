using Signa.OvaDesova.Api.Domain.Models;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IDadosGeraisService
    {
        DadosGeraisModel ConsultarDadosGerais(int tabelaPrecoFornecedorId);
        int Save(DadosGeraisModel dadosGerais);
        void Delete(int tabelaPrecoFornecedorId);
        DadosGeraisModel ConsultarHistorico(int tabelaPrecoFornecedorId);
    }
}