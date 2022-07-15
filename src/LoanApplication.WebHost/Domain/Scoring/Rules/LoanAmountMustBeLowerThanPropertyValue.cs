namespace LoanApplication.WebHost.Domain.Scoring.Rules;

public class LoanAmountMustBeLowerThanPropertyValue : IScoringRule
{
    public bool IsSatisfiedBy(LoanApplication loanApplication) =>
        loanApplication.Loan.LoanAmount < loanApplication.Property.Value;

    public string Message => "Property value is lower than loan amount.";
}
