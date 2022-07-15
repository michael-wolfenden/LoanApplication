using LoanApplication.WebHost.Domain;

namespace LoanApplication.WebHost.Infrastructure.Services;

public class SystemDateProvider : ICurrentDateProvider
{
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Now);
}
