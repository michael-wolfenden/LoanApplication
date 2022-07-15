using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Name : ValueObject
{
    private Name(string first, string last)
    {
        First = first;
        Last = last;
    }

    public string First { get; }
    public string Last { get; }

    public static Result<Name, Error> Of(string? first, string? last, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(first, nameof(First)).NotWhiteSpace().MaxLength(50)
            .Property(last, nameof(Last)).NotWhiteSpace().MaxLength(75)
            .Error;

        if (error is not null) return error;

        return new Name(first!, last!);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return First;
        yield return Last;
    }
}
