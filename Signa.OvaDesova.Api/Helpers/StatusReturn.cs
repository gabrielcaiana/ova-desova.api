using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Signa.OvaDesova.Api.Helpers
{
    public class StatusReturn
    {
        public static HttpRequestMessage Request { get; private set; }

        #region Ok
        public static HttpResponseMessage Ok()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public static HttpResponseMessage Ok(string texto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, texto);
        }

        public static HttpResponseMessage Ok(object objeto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, objeto);
        }
        #endregion

        #region BadRequest
        public static HttpResponseMessage BadRequest()
        {

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public static HttpResponseMessage BadRequest(string texto)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, texto);
        }

        public static HttpResponseMessage BadRequest(System.RuntimeTypeHandle objeto)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, objeto);
        }
        #endregion

        #region Unauthoried
        public static HttpResponseMessage Unauthorized()
        {
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        public static HttpResponseMessage Unauthorized(string texto)
        {
            return Request.CreateResponse(HttpStatusCode.Unauthorized, texto);
        }

        public static HttpResponseMessage Unauthorized(object objeto)
        {
            return Request.CreateResponse(HttpStatusCode.Unauthorized, objeto);
        }
        #endregion

        #region NotFound
        public static HttpResponseMessage NotFound()
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public static HttpResponseMessage NotFound(string texto)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, texto);
        }

        public static HttpResponseMessage NotFound(object objeto)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, objeto);
        }
        #endregion

        #region Forbidden
        public static HttpResponseMessage Forbidden()
        {
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        public static HttpResponseMessage Forbidden(string texto)
        {
            return Request.CreateResponse(HttpStatusCode.Forbidden, texto);
        }

        public static HttpResponseMessage Forbidden(object objeto)
        {
            return Request.CreateResponse(HttpStatusCode.Forbidden, objeto);
        }
        #endregion

        #region InternalServerError
        public static HttpResponseMessage InternalServerError()
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        public static HttpResponseMessage InternalServerError(string texto)
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError, texto);
        }

        public static HttpResponseMessage InternalServerError(object objeto)
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError, objeto);
        }
        #endregion

    }
}