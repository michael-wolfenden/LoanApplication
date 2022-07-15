using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class LoanNumberOfYears : SimpleValueObject<int>
{
    public LoanNumberOfYears(int value) : base(value)
    {
    }

    public static Result<LoanNumberOfYears, Error> Of(int value, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(value, nameof(LoanNumberOfYears)).Positive()
            .Error;

        if (error is not null) return error;

        return new LoanNumberOfYears(value);
    }
}
