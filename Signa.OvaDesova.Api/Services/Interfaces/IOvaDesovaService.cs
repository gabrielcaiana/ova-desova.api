using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IOvaDesovaService
    {
        IEnumerable<ResultadoModel> GetAll(ConsultaModel consulta);
        //TabelaPrecoFornecedorModel ConsultaTarifaPadrao(int tabelaPrecoFornecedorId);
        //TabelaPrecoFornecedorModel ConsultaTarifaEspecial(int tabelaPrecoFornecedorId);
        //TabelaPrecoFornecedorModel ConsultaMaterial(int tabelaPrecoFornecedorId);
    }
}