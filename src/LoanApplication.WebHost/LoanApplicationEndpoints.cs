using LoanApplication.WebHost.Domain.UseCases;
using LoanApplication.WebHost.Infrastructure.Web.CustomResults;
using MediatR;

namespace LoanApplication.WebHost;

public static class LoanApplicationEndpoints
{
    private const string Tag = "LoanApplication.WebHost";

    public static WebApplication MapLoanApplicationEndpoints(this WebApplication app)
    {
        app
            .MapPost("loan-application", SubmitLoanApplication)
            .WithTags(Tag)
            .Produces<SubmitLoanApplication.Response>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .WithName(nameof(SubmitLoanApplication));

        app
            .MapPut("loan-application/evaluate/{applicationNumber}", EvaluateLoanApplication)
            .WithTags(Tag)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .WithName(nameof(EvaluateLoanApplication));

        app
            .MapPut("loan-application/accept/{applicationNumber}", AcceptLoanApplication)
            .WithTags(Tag)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .WithName(nameof(AcceptLoanApplication));

        app
            .MapPut("loan-application/reject/{applicationNumber}", RejectLoanApplication)
            .WithTags(Tag)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .WithName(nameof(RejectLoanApplication));

        app
            .MapGet("loan-application/{applicationNumber}", GetLoanApplication)
            .WithTags(Tag)
            .Produces<GetLoanApplication.Response>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .WithName(nameof(GetLoanApplication));

        return app;
    }

    private static async Task<IResult> GetLoanApplication(IMediator mediator, string applicationNumber) =>
        Results.Extensions.FromResult(await mediator.Send(new GetLoanApplication.Request(applicationNumber)));

    private static async Task<IResult> SubmitLoanApplication(IMediator mediator, SubmitLoanApplication.Request request) =>
        Results.Extensions.FromResult(await mediator.Send(request));

    private static async Task<IResult> EvaluateLoanApplication(IMediator mediator, string applicationNumber) =>
        Results.Extensions.FromResult(await mediator.Send(new EvaluateLoanApplication.Request(applicationNumber)));

    private static async Task<IResult> AcceptLoanApplication(IMediator mediator, string applicationNumber) =>
        Results.Extensions.FromResult(await mediator.Send(new AcceptLoanApplication.Request(applicationNumber)));

    private static async Task<IResult> RejectLoanApplication(IMediator mediator, string applicationNumber) =>
        Results.Extensions.FromResult(await mediator.Send(new RejectLoanApplication.Request(applicationNumber)));
}
