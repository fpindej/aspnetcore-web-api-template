using Infrastructure.SqlServer.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.AddSqlServer(configuration);

        return services;
    }

    public static IServiceCollection AddApiDefinition(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck("Web API", () => HealthCheckResult.Healthy("App is running"), new[] { "ready", "api" });

        return services;
    }
}