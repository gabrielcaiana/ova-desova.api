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
    }
}