using Serilog;

namespace WebApiTemplate.WebApi.Extensions;

internal static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureLogging((builder, logging) =>
        {
            logging.ClearProviders();
            Logging.Extensions.LoggerConfigurationExtensions.SetupLoggerConfiguration(builder.Configuration);
        }).UseSerilog();

        return hostBuilder;
    }
}