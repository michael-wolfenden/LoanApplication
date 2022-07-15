using LoanApplication.WebHost.Domain;

namespace LoanApplication.WebHost.Infrastructure.Services;

public class AdminUserProvider : ICurrentUserProvider
{
    public string Username => "Admin";
}
