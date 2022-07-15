namespace LoanApplication.WebHost.Domain;

public interface ICurrentUserProvider
{
    public string? Username { get; }
}
