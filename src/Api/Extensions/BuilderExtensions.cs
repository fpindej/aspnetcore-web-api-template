namespace Api.Extensions;

internal static class BuilderExtensions
{
    public static void UseCustomizedSwagger(this IApplicationBuilder builder)
    {
        builder.UseSwagger();
        builder.UseSwaggerUI();
    }
}