using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

using static Math;
using static Enumerable;

public class Loan : ValueObject
{
    public Loan(LoanAmount loanAmount, LoanNumberOfYears loanNumberOfYears, Percent interestRate)
    {
        LoanAmount = loanAmount;
        LoanNumberOfYears = loanNumberOfYears;
        InterestRate = interestRate;
    }

    public LoanAmount LoanAmount { get; }
    public LoanNumberOfYears LoanNumberOfYears { get; }
    public Percent InterestRate { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return LoanAmount;
        yield return LoanNumberOfYears;
        yield return InterestRate;
    }

    public MonetaryAmount MonthlyInstallment()
    {
        var totalInstallments = LoanNumberOfYears * 12;

        var x = Range(1, totalInstallments).Sum(
            i => Pow(1.0 + ((double)InterestRate.Value / 100 / 12), -1 * i));

        return new MonetaryAmount(LoanAmount.Value / Convert.ToDecimal(x));
    }

    public DateOnly LastInstallmentsDate(DateOnly today) =>
        today.AddYears(LoanNumberOfYears);
}
