namespace LoanApplication.WebHost.Domain.Scoring.Rules;

public class CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65 : IScoringRule
{
    private readonly DateOnly _today;

    public CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65(DateOnly today) =>
        _today = today;

    public bool IsSatisfiedBy(LoanApplication loanApplication)
    {
        var lastInstallmentDate = loanApplication.Loan.LastInstallmentsDate(_today);
        return loanApplication.Customer.AgeAt(lastInstallmentDate) < Age.SixtyFive;
    }

    public string Message => "Customer age at last installment date is above 65.";
}
