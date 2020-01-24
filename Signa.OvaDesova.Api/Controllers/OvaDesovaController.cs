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
  public class OvaDesovaController : Controller
  {
    private readonly OvaDesovaBL _ovaDesovaBLL;

    public OvaDesovaController(OvaDesovaBL ovaDesovaBLL)
    {
      _ovaDesovaBLL = ovaDesovaBLL;
    }
    [HttpPost]
    [Route("Pesquisar")]
    public ActionResult GetAll(ConsultaModel consulta) => Ok(_ovaDesovaBLL.GetAll(consulta));

    [HttpGet]
    [Route("Delete")]
    public ActionResult Delete(int tabelaPrecoFornecedorId)
    {
      _ovaDesovaBLL.Delete(tabelaPrecoFornecedorId);
      return Ok();
    }
  }
}