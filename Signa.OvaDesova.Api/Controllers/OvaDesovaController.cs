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

        
    }
}