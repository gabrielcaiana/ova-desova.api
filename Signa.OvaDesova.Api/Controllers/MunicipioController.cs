using Signa.OvaDesova.Api.Filters;
using Signa.OvaDesova.Api.Services.Impls;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Controllers
{
    [ExceptionFilter]
    [Authorizate]
    public class MunicipioController : ApiController
    {
        IMunicipioService _service;

        public MunicipioController()
        {
            _service = new MunicipioService();
        }

        [HttpGet]
        [Route("Municipios")]
        public IHttpActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet]
        [Route("UFs")]
        public IHttpActionResult GetAllUf() => Ok(_service.GetAllUf());
    }
}