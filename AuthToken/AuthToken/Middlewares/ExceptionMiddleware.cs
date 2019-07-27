using AuthToken.ViewModels.Models.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace AuthToken.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApplicationException exception)
            {
                await HandleDefaultException(context, HttpStatusCode.BadRequest, exception);
            }
            catch (ValidationException exception)
            {
                await HandleDefaultException(context, HttpStatusCode.BadRequest, exception);
            }
            catch (Exception exception)
            {
               await HandleDefaultException(context, HttpStatusCode.InternalServerError);
            }
        }


        public async Task<string> HandleDefaultException(HttpContext context, HttpStatusCode httpStatusCode, Exception exception = null)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = @"application/json";
            var isCustomException = exception != null ? exception.GetType().IsSubclassOf(typeof(Exception)) : false;
            var responseString = string.Empty;
            var output = string.Empty;

            if (isCustomException)
            {
                responseString = exception?.Message;
                await context.Response.WriteAsync(responseString);
                output = JsonConvert.SerializeObject(new ErrorResponseView() {Error= responseString });
                return output;
            }

            responseString = new Exception("Server is not responding. Try later")?.Message;
            await context.Response.WriteAsync(responseString);
            output = JsonConvert.SerializeObject(new ErrorResponseView() { Error = responseString });
            return output;
        }
    }
}
