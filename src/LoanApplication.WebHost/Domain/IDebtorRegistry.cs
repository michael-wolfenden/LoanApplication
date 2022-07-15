namespace LoanApplication.WebHost.Domain;

public interface IDebtorRegistry
{
    bool IsRegisteredDebtor(Customer customer);
}
