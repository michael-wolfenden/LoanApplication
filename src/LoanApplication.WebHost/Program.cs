using LoanApplication.WebHost;
using LoanApplication.WebHost.Infrastructure;
using LoanApplication.WebHost.Infrastructure.Mediatr;
using LoanApplication.WebHost.Infrastructure.Persistence;
using LoanApplication.WebHost.Infrastructure.Services;
using LoanApplication.WebHost.Infrastructure.Web;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args)
        .AddCustomSerilog()
        .AddCustomMediatr()
        .AddCustomSwagger()
        .AddCustomExceptionHandling()
        .AddCustomPersistence()
        .AddCustomServices()
        .AddCustomWeb();

    var application = builder.Build()
        .UseCustomSerilog()
        .UseCustomExceptionHandling()
        .UseCustomSwagger()
        .MapLoanApplicationEndpoints();

    application.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "An exception was thrown during startup");

    return 1;
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
