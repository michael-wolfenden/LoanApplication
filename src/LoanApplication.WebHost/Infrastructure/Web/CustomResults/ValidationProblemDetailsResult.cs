using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.Options;

namespace LoanApplication.WebHost.Infrastructure.Web.CustomResults;

// We are using Hellang.Middleware.ProblemDetails which will handle any unhandled exceptions
// For consistency, we want to use the same ProblemDetailsFactory for returning our own ValidationProblemDetails
public class ValidationProblemDetailsResult : IResult
{
    private readonly IDictionary<string, string[]> _errors;

    public ValidationProblemDetailsResult(IDictionary<string, string[]> errors) =>
        _errors = errors;

    public Task ExecuteAsync(HttpContext httpContext)
    {
        var problemDetailsFactory = httpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
        var options = httpContext.RequestServices.GetRequiredService<IOptions<ProblemDetailsOptions>>().Value;

        var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(httpContext, _errors);
        problemDetails.Extensions[options.TraceIdPropertyName] = options.GetTraceId(httpContext);

        return Results.Problem(problemDetails).ExecuteAsync(httpContext);
    }
}
