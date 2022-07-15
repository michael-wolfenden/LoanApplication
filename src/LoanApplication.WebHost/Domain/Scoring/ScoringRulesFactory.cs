using LoanApplication.WebHost.Domain.Scoring.Rules;

namespace LoanApplication.WebHost.Domain.Scoring;

public class ScoringRulesFactory
{
    private readonly IDebtorRegistry _debtorRegistry;
    private readonly DateOnly _today;

    public ScoringRulesFactory(DateOnly today, IDebtorRegistry debtorRegistry)
    {
        _today = today;
        _debtorRegistry = debtorRegistry;
    }

    public ScoringRules DefaultSet => new(new List<IScoringRule>
    {
        new LoanAmountMustBeLowerThanPropertyValue(),
        new CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65(_today),
        new InstallmentAmountMustBeLowerThen15PercentOfCustomerIncome(),
        new CustomerIsNotARegisteredDebtor(_debtorRegistry)
    });
}
