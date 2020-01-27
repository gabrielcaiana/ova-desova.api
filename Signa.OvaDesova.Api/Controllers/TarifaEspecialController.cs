using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Business;

namespace Signa.TarifaEspecial.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize("Bearer")]
    [AllowAnonymous]
    public class TarifaEspecialController : Controller
    {
        private readonly TarifaEspecialBL _tarifaEspecialBLL;

        public TarifaEspecialController(TarifaEspecialBL tarifaEspecialBLL)
        {
            _tarifaEspecialBLL = tarifaEspecialBLL;
        }

        [HttpGet]
        [Route("TarifaEspecial")]
        public ActionResult ConsultarTarifaEspecial(int tabelaPrecoFornecedorId) => Ok(_tarifaEspecialBLL.ConsultarTarifaEspecial(tabelaPrecoFornecedorId));

        [HttpPost]
        [Route("TarifaEspecial")]
        public ActionResult Save(TarifaEspecialModel tarifaEspecial) => Ok(_tarifaEspecialBLL.Save(tarifaEspecial));

        [HttpGet]
        [Route("TarifaEspecial/Delete")]
        public ActionResult Delete(int tabelaTarifaEspecialId)
        {
            _tarifaEspecialBLL.Delete(tabelaTarifaEspecialId);
            return Ok();
        }

        [HttpGet]
        [Route("TarifaEspecial/Historico")]
        public ActionResult ConsultarHistorico(int tabelaTarifaEspecialId) => Ok(_tarifaEspecialBLL.ConsultarHistorico(tabelaTarifaEspecialId));

        [HttpGet]
        [Route("TarifaEspecial/Historico/Exclusao")]
        public ActionResult ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId) => Ok(_tarifaEspecialBLL.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
    }
}