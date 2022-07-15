using LoanApplication.WebHost.Domain;
using LoanApplication.WebHost.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LoanApplication.WebHost.Infrastructure.Services;

public class EntityFrameworkOperators : IOperators
{
    private readonly LoanDbContext _loanDbContext;

    public EntityFrameworkOperators(LoanDbContext loanDbContext) =>
        _loanDbContext = loanDbContext;

    public async Task<Operator?> WithLogin(Login login) =>
        await _loanDbContext.Operators.FirstOrDefaultAsync(o => o.Login == login);

    public async Task Add(Operator @operator) =>
        await _loanDbContext.Operators.AddAsync(@operator);
}
