using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Percent : SimpleValueObject<decimal>
{
    public static readonly Percent Fifteen = new(15);

    private Percent(decimal value) : base(value)
    {
    }

    public static Result<Percent, Error> Of(decimal number, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(number, nameof(Percent)).Positive()
            .Error;

        if (error is not null) return error;

        return new Percent(number);
    }
}
