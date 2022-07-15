using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class LoanApplicationId : SimpleValueObject<Guid>
{
    public LoanApplicationId(Guid value) : base(value)
    {
    }

    public static LoanApplicationId New() =>
        new(Guid.NewGuid());
}
