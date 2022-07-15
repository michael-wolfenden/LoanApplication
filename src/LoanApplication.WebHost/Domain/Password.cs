using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Password : SimpleValueObject<string>
{
    private Password(string value) : base(value) { }

    public static Result<Password, Error> Of(string? value, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(value, nameof(Password)).NotWhiteSpace().MaxLength(50)
            .Error;

        if (error is not null) return error;

        return new Password(value!);
    }
}
