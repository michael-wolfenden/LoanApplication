using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class PropertyValue : MonetaryAmount
{
    private PropertyValue(decimal amount) : base(amount)
    {
    }

    public static Result<PropertyValue, Error> Of(decimal value, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(value, nameof(PropertyValue)).Positive()
            .Error;

        if (error is not null) return error;

        return new PropertyValue(value);
    }
}
