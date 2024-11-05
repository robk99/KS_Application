using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;

namespace KS.Infrastructure.Middlewares
{
    public class ExceptionMiddlewareUI
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddlewareUI(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Error($"We encountered an InternalServerError Exception!: {ex.Message}");
                httpContext.Response.Redirect("/Error");
            }
        }
    }
}
