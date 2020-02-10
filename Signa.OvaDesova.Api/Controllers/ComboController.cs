using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signa.OvaDesova.Api.Business;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize("Bearer")]
    [AllowAnonymous]
    public class ComboController : Controller
    {
        private readonly ComboBL _comboBLL;

        public ComboController(ComboBL comboBLL)
        {
            _comboBLL = comboBLL;
        }

        [HttpGet]
        [Route("Fornecedores")]
        public ActionResult<IEnumerable<FornecedorModel>> GetAllFornecedor() => Ok(_comboBLL.GetAllFornecedor());

        [HttpGet]
        [Route("Municipios")]
        public ActionResult<IEnumerable<MunicipioModel>> GetAllMunicipio() => Ok(_comboBLL.GetAllMunicipio());

        [HttpGet]
        [Route("UFs")]
        public ActionResult<IEnumerable<UfModel>> GetAllUf() => Ok(_comboBLL.GetAllUf());

        [HttpGet]
        [Route("Veiculos")]
        public ActionResult<IEnumerable<VeiculoModel>> GetAllVeiculo() => Ok(_comboBLL.GetAllVeiculo());

        [HttpGet]
        [Route("AcordosRodoviarios")]
        public ActionResult<IEnumerable<AcordoRodoviarioModel>> GetAllAcordoRodoviario() => Ok(_comboBLL.GetAllAcordoRodoviario());

        [HttpGet]
        [Route("AcordosEspeciais")]
        public ActionResult<IEnumerable<AcordoEspecialModel>> GetAllAcordoEspecial(int tabTipoAcordoId) => Ok(_comboBLL.GetAllAcordoEspecial(tabTipoAcordoId));

        [HttpGet]
        [Route("FamiliasMercadoria")]
        public ActionResult<IEnumerable<FamiliaMercadoriaModel>> GetAllFamiliaMercadoria() => Ok(_comboBLL.GetAllFamiliaMercadoria());

        [HttpGet]
        [Route("Materiais")]
        public ActionResult<IEnumerable<MaterialModel>> GetAllMaterial() => Ok(_comboBLL.GetAllMaterial());

        [HttpGet]
        [Route("UnidadesMedida")]
        public ActionResult<IEnumerable<UnidadeMedidaModel>> GetAllUnidadeMedida() => Ok(_comboBLL.GetAllUnidadeMedida());
    }
}