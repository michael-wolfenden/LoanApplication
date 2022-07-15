namespace LoanApplication.WebHost.Domain.Scoring.Rules;

public class InstallmentAmountMustBeLowerThen15PercentOfCustomerIncome : IScoringRule
{
    public string Message => "Installment is higher than 15% of customer's income.";

    public bool IsSatisfiedBy(LoanApplication loanApplication) =>
        loanApplication.Loan.MonthlyInstallment() < loanApplication.Customer.MonthlyIncome * Percent.Fifteen;
}
