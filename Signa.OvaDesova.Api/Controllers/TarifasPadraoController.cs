using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signa.Library.Core.Aspnet.Domain.Models;
using Signa.OvaDesova.Api.Business;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Controllers
{
  [ApiController]
  [Produces("application/json")]
  [Authorize("Bearer")]
  [AllowAnonymous]
  public class TarifasPadraoController : Controller
  {
    private readonly TarifasPadraoBL _tarifasPadraoBLL;

    public TarifasPadraoController(TarifasPadraoBL tarifasPadraoBL)
    {
      _tarifasPadraoBLL = tarifasPadraoBL;
    }

    [HttpGet]
    [Route("TarifasPadrao")]
    public ActionResult ConsultarTarifasPadrao(int tabelaPrecoFornecedorId) => Ok(_tarifasPadraoBLL.ConsultarTarifasPadrao(tabelaPrecoFornecedorId));

    [HttpPost]
    [Route("TarifasPadrao")]
    public ActionResult Save(TarifasPadraoModel tarifasPadrao) => Ok(_tarifasPadraoBLL.Save(tarifasPadrao));

    [HttpGet]
    [Route("TarifasPadrao/Delete")]
    public ActionResult Delete(int tabelaOvaDesovaId)
    {
      _tarifasPadraoBLL.Delete(tabelaOvaDesovaId);
      return Ok();
    }

    [HttpGet]
    [Route("TarifasPadrao/Historico")]
    public ActionResult ConsultarHistorico(int tabelaOvaDesovaId) => Ok(_tarifasPadraoBLL.ConsultarHistorico(tabelaOvaDesovaId));

    [HttpGet]
    [Route("TarifasPadrao/Historico/Exclusao")]
    public ActionResult ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId) => Ok(_tarifasPadraoBLL.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
  }
}