using Serilog;
using LoggerConfigurationExtensions = WebApiTemplate.Logging.Extensions.LoggerConfigurationExtensions;

namespace WebApiTemplate.WebApi;

public static class Program
{
    public static int Main()
    {
        const string appName = "WebApiTemplate";
        LoggerConfigurationExtensions.SetupLoggerConfiguration();

        try
        {
            Log.Information("Starting web host {AppName}", appName);
            CreateHostBuilder().Build().Run();
            Log.Information("Ending web host {AppName}", appName);
            return 0;
        }
        catch (Exception e) when (e is not HostAbortedException)
        {
            Log.Fatal(e, "Host terminated unexpectedly {AppName}", appName);
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .UseSerilog(Log.Logger, true)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}