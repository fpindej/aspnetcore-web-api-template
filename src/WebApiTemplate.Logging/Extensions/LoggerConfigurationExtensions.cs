using Serilog;
using Serilog.Events;

namespace WebApiTemplate.Logging.Extensions;

public static class LoggerConfigurationExtensions
{
    public static void SetupLoggerConfiguration()
    {
        Log.Logger = new LoggerConfiguration()
            .ConfigureBaseLogging()
            .CreateLogger();
    }

    private static LoggerConfiguration ConfigureBaseLogging(
        this LoggerConfiguration loggerConfiguration)
    {
        loggerConfiguration
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console();

        return loggerConfiguration;
    }
}