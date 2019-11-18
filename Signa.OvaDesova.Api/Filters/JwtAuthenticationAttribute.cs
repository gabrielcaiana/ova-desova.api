//using Signa.Library;
//using Signa.Library.Security;
//using System;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http.Filters;
//using System.Web.Http.Results;

//namespace Signa.OvaDesova.Api.Filters
//{
//    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
//    {
//        public string Realm { get; set; }
//        public bool AllowMultiple => true;
//        private string token;

//        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
//        {
//            Global.NomeApi = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

//            var request = context.Request;
//            try
//            {
//                token = request.Headers.GetValues("token").First();

//            }
//            catch (Exception ex) { }


//            if (string.IsNullOrEmpty(token))
//            {
//                context.ErrorResult = new UnauthorizedResult(Enumerable.Empty<AuthenticationHeaderValue>(), context.Request);
//                return;
//            }
//            try
//            {
//                var valido = Token.VerificaValidade(token);

//                if (valido)
//                    return;

//            }
//            catch (Exception ex)
//            {
//                context.ErrorResult = new UnauthorizedResult(Enumerable.Empty<AuthenticationHeaderValue>(), context.Request);
//                return;
//            }
//            context.ErrorResult = new UnauthorizedResult(Enumerable.Empty<AuthenticationHeaderValue>(), context.Request);
//            return;

//        }

//        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
//        {
//            if (token != null)
//            {
//                Global.UsuarioId = int.Parse(Token.GetValor(token, "UsuarioId").ToString());
//                Global.EmpresaId = int.Parse(Token.GetValor(token, "EmpresaId").ToString());
//            }
//            return Task.FromResult(0);
//        }
//    }
//}