using CSharpFunctionalExtensions;
using LoanApplication.WebHost.Domain;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace LoanApplication.WebHost.Infrastructure.Web.CustomResults;

public static class ResultExtensions
{
    public static IResult FromResult<T>(this IResultExtensions _, Result<T, Error> result)
    {
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        var returnValidationResult = result.Error.Reasons.All(reason => reason.Type == ReasonType.Validation);

        if (returnValidationResult)
        {
            var errors = result.Error.Reasons
                .GroupBy(reason => reason.PropertyGroup ?? reason.Property)
                .ToDictionary(
                    group => group.Key,
                    reasons => reasons.Select(reason => reason.Message).ToArray());

            return new ValidationProblemDetailsResult(errors);
        }

        var error = result.Error.Reasons.Single();

        return error.Type == ReasonType.NotFound
            ? new ProblemDetailsResult(error.Message, statusCode: StatusCodes.Status404NotFound)
            : new ProblemDetailsResult(error.Message);
    }
}
