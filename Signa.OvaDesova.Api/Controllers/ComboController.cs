using Signa.OvaDesova.Api.Filters;
using Signa.OvaDesova.Api.Services.Impls;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Controllers
{
    [ExceptionFilter]
    [Authorizate]
    public class ComboController : ApiController
    {
        IComboService _service;

        public ComboController()
        {
            _service = new ComboService();
        }

        [HttpGet]
        [Route("Fornecedores")]
        public IHttpActionResult GetAllFornecedor() => Ok(_service.GetAllFornecedor());

        [HttpGet]
        [Route("Municipios")]
        public IHttpActionResult GetAllMunicipio() => Ok(_service.GetAllMunicipio());

        [HttpGet]
        [Route("UFs")]
        public IHttpActionResult GetAllUf() => Ok(_service.GetAllUf());

        [HttpGet]
        [Route("Veiculos")]
        public IHttpActionResult GetAllVeiculo() => Ok(_service.GetAllVeiculo());

        [HttpGet]
        [Route("AcordosRodoviarios")]
        public IHttpActionResult GetAllAcordoRodoviario() => Ok(_service.GetAllAcordoRodoviario());

        [HttpGet]
        [Route("AcordosEspeciais")]
        public IHttpActionResult GetAllAcordoEspecial() => Ok(_service.GetAllAcordoEspecial());

        [HttpGet]
        [Route("FamiliasMercadoria")]
        public IHttpActionResult GetAllFamiliaMercadoria() => Ok(_service.GetAllFamiliaMercadoria());

        [HttpGet]
        [Route("Materiais")]
        public IHttpActionResult GetAllMaterial() => Ok(_service.GetAllMaterial());

        [HttpGet]
        [Route("UnidadesMedida")]
        public IHttpActionResult GetAllUnidadeMedida() => Ok(_service.GetAllUnidadeMedida());
    }
}