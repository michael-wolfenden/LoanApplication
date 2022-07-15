using LoanApplication.WebHost.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanApplication.WebHost.Infrastructure.Mediatr.Behaviours;

public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly LoanDbContext _loanDbContext;

    public UnitOfWorkBehaviour(LoanDbContext loanDbContext) =>
        _loanDbContext = loanDbContext;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var strategy = _loanDbContext.Database.CreateExecutionStrategy();

        var response = await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await _loanDbContext.Database.BeginTransactionAsync(cancellationToken);
            var response = await next();

            await _loanDbContext.SaveChangesAsync(cancellationToken);
            await tx.CommitAsync(cancellationToken);

            return response;
        });

        return response;
    }
}
