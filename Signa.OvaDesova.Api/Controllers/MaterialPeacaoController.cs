using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Filters;
using Signa.OvaDesova.Api.Services.Impls;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Controllers
{
    [ExceptionFilter]
    [Authorizate]
    public class MaterialPeacaoController : ApiController
    {
        IMaterialPeacaoService _service;

        public MaterialPeacaoController()
        {
            _service = new MaterialPeacaoService();
        }

        [HttpGet]
        [Route("MaterialPeacao")]
        public IHttpActionResult ConsultarMaterialPeacao(int tabelaPrecoFornecedorId) => Ok(_service.ConsultarMaterialPeacao(tabelaPrecoFornecedorId));

        [HttpPost]
        [Route("MaterialPeacao")]
        public IHttpActionResult Save(MaterialPeacaoModel materialPeacao) => Ok(_service.Save(materialPeacao));

        [HttpGet]
        [Route("MaterialPeacao/Delete")]
        public IHttpActionResult Delete(int tabelaTarifaMaterialId)
        {
            _service.Delete(tabelaTarifaMaterialId);
            return Ok();
        }

        [HttpGet]
        [Route("MaterialPeacao/Historico")]
        public IHttpActionResult ConsultarHistorico(int tabelaTarifaMaterialId) => Ok(_service.ConsultarHistorico(tabelaTarifaMaterialId));

        [HttpGet]
        [Route("MaterialPeacao/Historico/Exclusao")]
        public IHttpActionResult ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId) => Ok(_service.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
    }
}