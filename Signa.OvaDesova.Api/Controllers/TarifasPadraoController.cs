using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Filters;
using Signa.OvaDesova.Api.Services.Impls;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Controllers
{
    [ExceptionFilter]
    [Authorizate]
    public class TarifasPadraoController : ApiController
    {
        ITarifasPadraoService _service;

        public TarifasPadraoController()
        {
            _service = new TarifasPadraoService();
        }

        [HttpGet]
        [Route("TarifasPadrao")]
        public IHttpActionResult ConsultarTarifasPadrao(int tabelaOvaDesovaId) => Ok(_service.ConsultarTarifasPadrao(tabelaOvaDesovaId));

        [HttpPost]
        [Route("TarifasPadrao")]
        public IHttpActionResult Save(TarifasPadraoModel tarifasPadrao) => Ok(_service.Save(tarifasPadrao));

        [HttpGet]
        [Route("TarifasPadrao/Delete")]
        public IHttpActionResult Delete(int tabelaOvaDesovaId)
        {
            _service.Delete(tabelaOvaDesovaId);
            return Ok();
        }
    }
}