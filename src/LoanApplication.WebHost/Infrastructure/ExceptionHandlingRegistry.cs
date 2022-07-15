using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LoanApplication.WebHost.Infrastructure;

public static class ExceptionHandlingRegistry
{
    public static WebApplicationBuilder AddCustomExceptionHandling(this WebApplicationBuilder builder)
    {
        builder.Services.TryAddSingleton<IActionResultExecutor<ObjectResult>, MinimalApiProblemDetailsResultExecutor>();
        builder.Services.AddProblemDetails();

        return builder;
    }

    public static WebApplication UseCustomExceptionHandling(this WebApplication app)
    {
        app.UseProblemDetails();

        return app;
    }

    private sealed class MinimalApiProblemDetailsResultExecutor : IActionResultExecutor<ObjectResult>
    {
        public Task ExecuteAsync(ActionContext context, ObjectResult result) =>
            Results.Json(result.Value, null, "application/problem+json", result.StatusCode)
                .ExecuteAsync(context.HttpContext);
    }
}
