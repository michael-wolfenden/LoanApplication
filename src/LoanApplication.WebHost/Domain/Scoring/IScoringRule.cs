namespace LoanApplication.WebHost.Domain.Scoring;

public interface IScoringRule
{
    string Message { get; }

    bool IsSatisfiedBy(LoanApplication loanApplication);
}
