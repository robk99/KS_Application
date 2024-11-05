using KS.Application.Exception;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace KS.Infrastructure.Middlewares
{
    public class ResponseExceptionHandler
    {
        public Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ExceptionDTO()
            {
                StatusCode = context.Response.StatusCode,
                Message = "We're having some server difficulties. Out team is notified and will try to fix this problem as soon as possible."
            }.ToString());
        }
    }
}
