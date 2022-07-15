using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class OperatorId : SimpleValueObject<Guid>
{
    public OperatorId(Guid value) : base(value)
    {
    }

    public static OperatorId New() =>
        new(Guid.NewGuid());
}
