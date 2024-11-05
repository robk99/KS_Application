using Microsoft.AspNetCore.Http;
using Serilog;

namespace KS.Infrastructure.Middlewares
{
    public class ExceptionMiddlewareAPI
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddlewareAPI(RequestDelegate next, ILogger logger)
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
                await new ResponseExceptionHandler().HandleExceptionAsync(httpContext);
            }
        }
    }
}
