using Signa.OvaDesova.Api.Filters;
using Signa.OvaDesova.Api.Services.Impls;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Controllers
{
    [ExceptionFilter]
    [Authorizate]
    public class FornecedorController : ApiController
    {
        IFornecedorService _service;

        public FornecedorController()
        {
            _service = new FornecedorService();
        }

        [HttpGet]
        [Route("Fornecedores")]
        public IHttpActionResult GetAll() => Ok(_service.GetAll());
    }
}