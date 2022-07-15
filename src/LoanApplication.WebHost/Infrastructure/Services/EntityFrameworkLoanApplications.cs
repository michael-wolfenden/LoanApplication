using LoanApplication.WebHost.Domain;
using LoanApplication.WebHost.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LoanApplication.WebHost.Infrastructure.Services;

public class EntityFrameworkLoanApplications : ILoanApplications
{
    private readonly LoanDbContext _loanDbContext;

    public EntityFrameworkLoanApplications(LoanDbContext loanDbContext) =>
        _loanDbContext = loanDbContext;

    public async Task<Domain.LoanApplication?> WithNumber(LoanApplicationNumber loanApplicationNumber) =>
        await _loanDbContext.LoanApplications.FirstOrDefaultAsync(l => l.Number == loanApplicationNumber);

    public async Task Add(Domain.LoanApplication loanApplication) =>
        await _loanDbContext.LoanApplications.AddAsync(loanApplication);
}
