using LoanApplication.WebHost.Domain;

namespace LoanApplication.WebHost.Infrastructure.Services;

public class HardCodedDebtorRegistry : IDebtorRegistry
{
    public bool IsRegisteredDebtor(Customer customer) =>
        customer.NationalIdentifier == "11111111111";
}
