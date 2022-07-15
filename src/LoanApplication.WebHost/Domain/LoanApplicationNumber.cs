using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class LoanApplicationNumber : SimpleValueObject<string>
{
    private LoanApplicationNumber(string value) : base(value) { }

    public static Result<LoanApplicationNumber, Error> Of(string? value, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(value, nameof(LoanApplicationNumber)).NotWhiteSpace().MaxLength(50)
            .Error;

        if (error is not null) return error;

        return new LoanApplicationNumber(value!);
    }

    public static LoanApplicationNumber New() =>
        new(Guid.NewGuid().ToString());
}
