using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace WebApiTemplate.Logging.Extensions;

public static class LoggerConfigurationExtensions
{
    public static void SetupLoggerConfiguration(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ConfigureBaseLogging()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    public static Logger SetupBaseLogger()
    {
        var baseConfiguration = new LoggerConfiguration()
            .ConfigureBaseLogging();
        
        return baseConfiguration.CreateLogger();
    }

    private static LoggerConfiguration ConfigureBaseLogging(this LoggerConfiguration loggerConfiguration)
    {
        return loggerConfiguration
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console();
    }
}