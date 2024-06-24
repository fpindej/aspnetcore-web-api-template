using Serilog;
using WebApiTemplate.WebApi.Extensions;
using LoggerConfigurationExtensions = WebApiTemplate.Logging.Extensions.LoggerConfigurationExtensions;

namespace WebApiTemplate.WebApi;

public static class Program
{
    public static async Task<int> Main()
    {
        const string appName = "WebApiTemplate";
        await using var baseLogger = LoggerConfigurationExtensions.SetupBaseLogger();

        try
        {
            baseLogger.Information("Starting web host {AppName}", appName);
            await CreateHostBuilder().Build().RunAsync();
            baseLogger.Information("Ending web host {AppName}", appName);
            return 0;
            
        }
        catch (Exception e) when (e is not HostAbortedException)
        {
            baseLogger.Fatal(e, "Host terminated unexpectedly {AppName}", appName);
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureSerilog()
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}