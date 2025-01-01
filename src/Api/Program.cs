using Api.Extensions;
using Api.Middlewares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Production;
Log.Logger = Logging.Extensions.LoggerConfigurationExtensions.ConfigureMinimalLogging(environmentName);
const string appName = "Web Api Template";

try
{
    Log.Information("Starting web host: {appName}. Environment: {env}", appName, environmentName);
    var builder = WebApplication.CreateBuilder(args);

    Log.Debug("Use Serilog");
    builder.Host.UseSerilog(
        (context, _, loggerConfiguration) =>
        {
            Logging.Extensions.LoggerConfigurationExtensions.SetupLogger(context.Configuration, loggerConfiguration);
        }, preserveStaticLogger: true);

    Log.Debug("Adding Services");
    builder.Services.AddServices(builder.Configuration);

    builder.Services.AddHealthChecks(builder.Configuration);

    Log.Debug("Adding Controllers");
    builder.Services.AddControllers();

    Log.Debug("Adding Swagger for API documentation");
    builder.Services.AddApiDefinition();

    var app = builder.Build();

    Log.Debug("Configuring CORS policy to allow any origin, header, and method");
    app.UseCors(opt =>
    {
        opt.SetIsOriginAllowed(_ => true);
        opt.AllowAnyHeader();
        opt.AllowAnyMethod();
    });
    
    Log.Debug("Enabling Swagger and SwaggerUI for API documentation");
    app.UseCustomizedSwagger();

    if (app.Environment.IsDevelopment())
    {
        Log.Debug("Development environment detected");

        Log.Debug("Enabling Developer Exception Page");
        app.UseDeveloperExceptionPage();
    }

    Log.Debug("Enabling HTTPS redirection");
    app.UseHttpsRedirection();

    Log.Debug("Configuring request logging with Serilog");
    app.UseSerilogRequestLogging();

    Log.Debug("Adding custom exception handling middleware");
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    Log.Debug("Configuring Authorization");
    app.UseAuthorization();

    Log.Debug("Mapping controller routes");
    app.MapControllers();

    Log.Debug("Mapping health checks");
    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    await app.RunAsync();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Shutting down application");
    await Log.CloseAndFlushAsync();
}