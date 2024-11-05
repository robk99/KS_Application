using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KS.Infrastructure.Logging
{
    public static class LoggerConfigurator
    {
        public static void ConfigureLogging(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            hostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration
                    .WriteTo.Console()
                    .ReadFrom.Configuration(configuration);
            });
        }
    }
}
