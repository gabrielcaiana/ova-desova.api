using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IOvaDesovaService
    {
        IEnumerable<TabelaPrecoFornecedorModel> GetAll(ConsultaModel consulta);
        //TabelaPrecoFornecedorModel ConsultaDadosGerais(int tabelaPrecoFornecedorId);
        //TabelaPrecoFornecedorModel ConsultaTarifaPadrao(int tabelaPrecoFornecedorId);
        //TabelaPrecoFornecedorModel ConsultaTarifaEspecial(int tabelaPrecoFornecedorId);
        //TabelaPrecoFornecedorModel ConsultaMaterial(int tabelaPrecoFornecedorId);
    }
}