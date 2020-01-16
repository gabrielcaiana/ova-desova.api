using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Filters;
using Signa.OvaDesova.Api.Services.Impls;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Controllers
{
    [ExceptionFilter]
    [Authorizate]
    public class TarifaEspecialController : ApiController
    {
        ITarifaEspecialService _service;

        public TarifaEspecialController()
        {
            _service = new TarifaEspecialService();
        }

        [HttpGet]
        [Route("TarifaEspecial")]
        public IHttpActionResult ConsultarTarifaEspecial(int tabelaPrecoFornecedorId) => Ok(_service.ConsultarTarifaEspecial(tabelaPrecoFornecedorId));

        [HttpPost]
        [Route("TarifaEspecial")]
        public IHttpActionResult Save(TarifaEspecialModel tarifaEspecial) => Ok(_service.Save(tarifaEspecial));

        [HttpGet]
        [Route("TarifaEspecial/Delete")]
        public IHttpActionResult Delete(int tabelaTarifaEspecialId)
        {
            _service.Delete(tabelaTarifaEspecialId);
            return Ok();
        }

        [HttpGet]
        [Route("TarifaEspecial/Historico")]
        public IHttpActionResult ConsultarHistorico(int tabelaTarifaEspecialId) => Ok(_service.ConsultarHistorico(tabelaTarifaEspecialId));

        [HttpGet]
        [Route("TarifaEspecial/Historico/Exclusao")]
        public IHttpActionResult ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId) => Ok(_service.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
    }
}