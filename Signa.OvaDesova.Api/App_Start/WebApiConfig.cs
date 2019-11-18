using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Signa.Library;
using Signa.Library.Extensions;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Signa.OvaDesova.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            string nomeApi = ConfigurationManager.AppSettings["NomeApi"];
            Global.NomeApi = nomeApi.IsNullEmptyOrWhiteSpace() ? "NomePadrãoApi" : nomeApi;
            Global.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString + "Api:" + Global.NomeApi;

            string sFuncaoId = ConfigurationManager.AppSettings["FuncaoId"];
            Global.FuncaoId = sFuncaoId.IsNullEmptyOrWhiteSpace() ? 0 : int.Parse(sFuncaoId); //trocar 0 pelo padrão da função

            var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
