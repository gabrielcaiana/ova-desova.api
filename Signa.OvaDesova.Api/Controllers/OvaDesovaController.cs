using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Filters;
using Signa.OvaDesova.Api.Services.Impls;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Controllers
{
    [ExceptionFilter]
    [Authorizate]
    public class OvaDesovaController : ApiController
    {
        IOvaDesovaService _service;

        public OvaDesovaController()
        {
            _service = new OvaDesovaService();
        }

        [HttpPost]
        [Route("Pesquisar")]
        public IHttpActionResult GetAll(ConsultaModel consulta) => Ok(_service.GetAll(consulta));

        //[HttpPost]
        //[Route("ConsultaTarifaPadrao")]
        //public IHttpActionResult ConsultaTarifaPadrao(int tabelaPrecoFornecedorId) => Ok(_service.ConsultaTarifaPadrao(tabelaPrecoFornecedorId));

        //[HttpPost]
        //[Route("ConsultaTarifaEspecial")]
        //public IHttpActionResult ConsultaTarifaEspecial(int tabelaPrecoFornecedorId) => Ok(_service.ConsultaTarifaEspecial(tabelaPrecoFornecedorId));

        //[HttpPost]
        //[Route("ConsultaMaterial")]
        //public IHttpActionResult ConsultaMaterial(int tabelaPrecoFornecedorId) => Ok(_service.ConsultaMaterial(tabelaPrecoFornecedorId));
    }
}