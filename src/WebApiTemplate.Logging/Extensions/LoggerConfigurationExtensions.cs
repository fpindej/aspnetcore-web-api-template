using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace WebApiTemplate.Logging.Extensions;

public static class LoggerConfigurationExtensions
{
    public static void SetupLogger()
    {
        var baseConfiguration = new LoggerConfiguration()
            .ConfigureBaseLogging();
        
        Log.Logger = baseConfiguration.CreateLogger();
    }

    private static LoggerConfiguration ConfigureBaseLogging(this LoggerConfiguration loggerConfiguration)
    {
        return loggerConfiguration
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message:lj}{NewLine}{Exception:j}"))
            .Enrich.FromLogContext();
    }
}