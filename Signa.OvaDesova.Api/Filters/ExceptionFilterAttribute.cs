using Signa.Library;
using Signa.Library.Exceptions;
using Signa.Library.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Signa.OvaDesova.Api.Filters
{
    interface IExceptionHandling
    {
        object TratarErro(Exception ex);
    }

    class SignaRegraNegocioHandling : IExceptionHandling
    {
        public object TratarErro(Exception ex)
        {
            return new { ex.Message };
        }
    }

    class ValidationHandling : IExceptionHandling
    {
        public object TratarErro(Exception ex)
        {
            var e = ex as FluentValidation.ValidationException;

            if (e == null)
            {
                return e;
            }

            var errors = e.Errors.Select(x => x.ErrorMessage).ToArray();

            return new { Message = string.Join("\n", errors) };
        }
    }

    class GenericHandling : IExceptionHandling
    {
        public object TratarErro(Exception ex)
        {
            var logMsgId = 0;
            var mensagemException = ex.Message;
            var stackTrace = ex.StackTrace;

            try
            {
                logMsgId = CommonHelper.GravaLogMsg(1, Global.UsuarioId, Global.FuncaoId, texto: $"{(stackTrace != null ? stackTrace.ToString() : "")} ({Global.NomeApi} U:{Global.UsuarioId})");
            }
            catch (Exception)
            {
                try
                {
                    EventLog.WriteEntry(Global.NomeApi, $"{stackTrace.ToString()}U:{Global.UsuarioId}", EventLogEntryType.Error, 234);
                }
                catch (Exception)
                {
                }
            }

            return new
            {
                Message = $"Problemas {(Global.UsuarioId == 1 ? $"na rotina {Global.NomeApi}" : "no sistema")}. Entre em contato com o administrador do sistema",
                MessageException = mensagemException,
                StackTrace = stackTrace,
                logMsgId
            };
        }
    }

    public class ExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        bool IFilter.AllowMultiple => true;
        private IExceptionHandling _exceptionHandling;

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var exception = actionExecutedContext.Exception;

            _exceptionHandling = ExecuteExceptionFilter(exception);

            var retorno = _exceptionHandling.TratarErro(exception);

            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent(new object().GetType(), retorno, new JsonMediaTypeFormatter())
            };
            return Task.FromResult(0);
        }

        private IExceptionHandling ExecuteExceptionFilter(Exception ex)
        {
            if (ex is SignaRegraNegocioException)
            {
                return new SignaRegraNegocioHandling();
            }

            if (ex is FluentValidation.ValidationException)
            {
                return new ValidationHandling();
            }

            return new GenericHandling();
        }
    }
}