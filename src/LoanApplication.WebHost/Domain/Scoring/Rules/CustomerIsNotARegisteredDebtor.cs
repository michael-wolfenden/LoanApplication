namespace LoanApplication.WebHost.Domain.Scoring.Rules;

public class CustomerIsNotARegisteredDebtor : IScoringRule
{
    private readonly IDebtorRegistry _debtorRegistry;

    public CustomerIsNotARegisteredDebtor(IDebtorRegistry debtorRegistry) =>
        _debtorRegistry = debtorRegistry;

    public string Message => "Customer is registered in debtor registry";

    public bool IsSatisfiedBy(LoanApplication loanApplication) =>
        !_debtorRegistry.IsRegisteredDebtor(loanApplication.Customer);
}
