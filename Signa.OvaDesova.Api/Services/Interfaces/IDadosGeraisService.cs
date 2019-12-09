using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IDadosGeraisService
    {
        DadosGeraisModel ConsultarDadosGerais(int tabelaPrecoFornecedorId);
        int Save(DadosGeraisModel dadosGerais);
        void Delete(int tabelaPrecoFornecedorId);
        IEnumerable<DadosGeraisModel> ConsultarHistorico(int tabelaPrecoFornecedorId);
    }
}