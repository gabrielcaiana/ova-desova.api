using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Filters;
using Signa.OvaDesova.Api.Services.Impls;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Controllers
{
    [ExceptionFilter]
    [Authorizate]
    public class DadosGeraisController : ApiController
    {
        IDadosGeraisService _service;

        public DadosGeraisController()
        {
            _service = new DadosGeraisService();
        }

        [HttpGet]
        [Route("DadosGerais")]
        public IHttpActionResult ConsultarDadosGerais(int tabelaPrecoFornecedorId) => Ok(_service.ConsultarDadosGerais(tabelaPrecoFornecedorId));

        [HttpPost]
        [Route("DadosGerais")]
        public IHttpActionResult Save(DadosGeraisModel dadosGerais) => Ok(_service.Save(dadosGerais));

        [HttpGet]
        [Route("DadosGerais/Delete")]
        public IHttpActionResult Delete(int tabelaPrecoFornecedorId)
        {
            _service.Delete(tabelaPrecoFornecedorId);
            return Ok();
        }

        [HttpGet]
        [Route("DadosGerais/Historico")]
        public IHttpActionResult ConsultarHistorico(int tabelaPrecoFornecedorId) => Ok(_service.ConsultarHistorico(tabelaPrecoFornecedorId));

        [HttpGet]
        [Route("DadosGerais/Historico/Exclusao")]
        public IHttpActionResult ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId) => Ok(_service.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
    }
}