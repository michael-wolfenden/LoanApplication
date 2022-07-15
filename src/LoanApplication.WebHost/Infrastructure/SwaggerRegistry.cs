namespace LoanApplication.WebHost.Infrastructure;

public static class SwaggerRegistry
{
    public static WebApplicationBuilder AddCustomSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(type => type.FullName?.Replace("+", ""));
        });

        return builder;
    }

    public static WebApplication UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(o =>
        {
            o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            o.RoutePrefix = string.Empty;
        });

        return app;
    }
}
