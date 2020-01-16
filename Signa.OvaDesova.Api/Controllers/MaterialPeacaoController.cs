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
  public class MaterialPeacaoController : Controller
  {
    private readonly MaterialPeacaoBL _materialPeacaoBLL;

    public MaterialPeacaoController(MaterialPeacaoBL materialPeacaoBLL)
    {
      _materialPeacaoBLL = materialPeacaoBLL;
    }

    [HttpGet]
    [Route("MaterialPeacao")]
    public ActionResult ConsultarMaterialPeacao(int tabelaPrecoFornecedorId) => Ok(_materialPeacaoBLL.ConsultarMaterialPeacao(tabelaPrecoFornecedorId));

    [HttpPost]
    [Route("MaterialPeacao")]
    public ActionResult Save(MaterialPeacaoModel materialPeacao) => Ok(_materialPeacaoBLL.Save(materialPeacao));

    [HttpGet]
    [Route("MaterialPeacao/Delete")]
    public ActionResult Delete(int tabelaTarifaMaterialId)
    {
      _materialPeacaoBLL.Delete(tabelaTarifaMaterialId);
      return Ok();
    }

    [HttpGet]
    [Route("MaterialPeacao/Historico")]
    public ActionResult ConsultarHistorico(int tabelaTarifaMaterialId) => Ok(_materialPeacaoBLL.ConsultarHistorico(tabelaTarifaMaterialId));

    [HttpGet]
    [Route("MaterialPeacao/Historico/Exclusao")]
    public ActionResult ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId) => Ok(_materialPeacaoBLL.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
  }
}