using Signa.Library;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Signa.OvaDesova.Api.Filters
{
    public class AuthorizateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.Request;

            try
            {
                var usuarioId = request.Headers.GetValues("UsuarioId").First();
                Global.UsuarioId = int.Parse(usuarioId);
            }
            catch (Exception) { }
        }
    }
}