namespace LoanApplication.WebHost.Domain;

public interface ICurrentDateProvider
{
    DateOnly Today { get; }
}
