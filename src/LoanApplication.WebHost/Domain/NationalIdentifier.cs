using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class NationalIdentifier : SimpleValueObject<string>
{
    private NationalIdentifier(string value) : base(value) { }

    public static Result<NationalIdentifier, Error> Of(string? value, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(value, nameof(NationalIdentifier)).NotWhiteSpace().Length(11)
            .Error;

        if (error is not null) return error;

        return new NationalIdentifier(value!);
    }
}
