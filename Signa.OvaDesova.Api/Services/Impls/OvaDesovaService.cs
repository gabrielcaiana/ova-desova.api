using Signa.Default.Template.Domain.Validators;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Impls
{
    class OvaDesovaService : IOvaDesovaService
    {
        IOvaDesovaRepository _ovaDesova;
        OvaDesovaValidator _ovaDesovaValidator;

        public OvaDesovaService()
        {
            _ovaDesova = new OvaDesovaRepository();
            _ovaDesovaValidator = new OvaDesovaValidator();
        }

        public IEnumerable<ResultadoModel> GetAll(ConsultaModel consulta)
        {
            return _ovaDesova.GetAll(consulta);
        }

        public TabelaPrecoFornecedorModel ConsultaDadosGerais(int tabelaPrecoFornecedorId)
        {
            return _ovaDesova.ConsultaDadosGerais(tabelaPrecoFornecedorId);
        }
    }
}