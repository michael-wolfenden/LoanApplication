using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class LoanAmount : MonetaryAmount
{
    private LoanAmount(decimal amount) : base(amount)
    {
    }

    public static Result<LoanAmount, Error> Of(decimal value, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(value, nameof(LoanAmount)).Positive()
            .Error;

        if (error is not null) return error;

        return new LoanAmount(value);
    }
}
