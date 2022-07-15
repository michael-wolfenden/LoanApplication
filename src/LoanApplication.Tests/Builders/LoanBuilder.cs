using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Builders;

public class LoanBuilder
{
    private Percent? _interestRate;
    private LoanAmount? _loanAmount;
    private LoanNumberOfYears? _loanNumberOfYears;

    public LoanBuilder()
    {
        WithLoanAmount(200_000M);
        WithLoanNumberOfYears(20);
        WithInterestRate(1);
    }

    public static LoanBuilder GivenLoan() =>
        new();

    public LoanBuilder WithLoanAmount(decimal loanAmount) =>
        Set(x => x._loanAmount = LoanAmount.Of(loanAmount).Value);

    public LoanBuilder WithLoanNumberOfYears(int loanNumberOfYears) =>
        Set(x => x._loanNumberOfYears = LoanNumberOfYears.Of(loanNumberOfYears).Value);

    public LoanBuilder WithInterestRate(decimal interestRate) =>
        Set(x => x._interestRate = Percent.Of(interestRate).Value);

    private LoanBuilder Set(Action<LoanBuilder> setter)
    {
        setter(this);
        return this;
    }

    public Loan Build() =>
        new(_loanAmount!, _loanNumberOfYears!, _interestRate!);
}
