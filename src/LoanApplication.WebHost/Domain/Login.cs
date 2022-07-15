using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Login : SimpleValueObject<string>
{
    private Login(string value) : base(value) { }

    public static Result<Login, Error> Of(string? value, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(value, nameof(Login)).NotWhiteSpace().MaxLength(50)
            .Error;

        if (error is not null) return error;

        return new Login(value!);
    }
}
