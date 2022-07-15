using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.Options;

namespace LoanApplication.WebHost.Infrastructure.Web.CustomResults;

// We are using Hellang.Middleware.ProblemDetails which will handle any unhandled exceptions
// For consistency, we want to use the same ProblemDetailsFactory for returning our own ProblemDetails
public class ProblemDetailsResult : IResult
{
    private readonly string? _detail;
    private readonly IDictionary<string, object?>? _extensions;
    private readonly string? _instance;
    private readonly int? _statusCode;
    private readonly string? _title;
    private readonly string? _type;

    public ProblemDetailsResult(
        string? detail = null,
        string? instance = null,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        IDictionary<string, object?>? extensions = null)
    {
        _detail = detail;
        _instance = instance;
        _statusCode = statusCode;
        _title = title;
        _type = type;
        _extensions = extensions;
    }


    public Task ExecuteAsync(HttpContext httpContext)
    {
        var problemDetailsFactory = httpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
        var options = httpContext.RequestServices.GetRequiredService<IOptions<ProblemDetailsOptions>>().Value;

        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            httpContext,
            _statusCode ?? options.ValidationProblemStatusCode,
            _title,
            _type,
            _detail,
            _instance);

        if (_extensions is not null)
            foreach (var extension in _extensions)
                problemDetails.Extensions.Add(extension);

        problemDetails.Extensions[options.TraceIdPropertyName] = options.GetTraceId(httpContext);

        return Results.Problem(problemDetails).ExecuteAsync(httpContext);
    }
}
