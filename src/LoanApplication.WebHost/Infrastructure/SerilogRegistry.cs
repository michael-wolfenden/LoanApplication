using System.Diagnostics;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace LoanApplication.WebHost.Infrastructure;

public static class SerilogRegistry
{
    public static WebApplicationBuilder AddCustomSerilog(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<TraceIdEnricher>();

        builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithProperty(nameof(builder.Environment.EnvironmentName), builder.Environment.EnvironmentName)
            .Enrich.WithProperty(nameof(builder.Environment.ApplicationName), builder.Environment.ApplicationName)
            .Enrich.With(services.GetRequiredService<TraceIdEnricher>())
            .WriteTo.Console()
            .ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    public static WebApplication UseCustomSerilog(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        return app;
    }

    private class TraceIdEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TraceIdEnricher(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
        {
            var traceId = Activity.Current?.Id ?? _httpContextAccessor.HttpContext?.TraceIdentifier;
            logEvent.AddPropertyIfAbsent(factory.CreateProperty(nameof(Activity.TraceId), traceId));
        }
    }
}
