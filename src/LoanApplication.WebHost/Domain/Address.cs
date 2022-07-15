using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Address : ValueObject
{
    private Address(string country, string zipCode, string city, string street)
    {
        Country = country;
        ZipCode = zipCode;
        City = city;
        Street = street;
    }

    public string Country { get; }
    public string ZipCode { get; }
    public string City { get; }
    public string Street { get; }

    public static Result<Address, Error> Of(string? country, string? zipCode, string? city, string? street, string? propertyGroup = null)
    {
        var error = Validate
            .WithPropertyGroup(propertyGroup)
            .Property(country, nameof(Country)).NotWhiteSpace().MaxLength(50)
            .Property(zipCode, nameof(ZipCode)).NotWhiteSpace().MaxLength(6).Matches("[0-9]{2}-[0-9]{3}")
            .Property(city, nameof(City)).NotWhiteSpace().MaxLength(50)
            .Property(street, nameof(Street)).NotWhiteSpace().MaxLength(50)
            .Error;

        if (error is not null) return error;

        // unfortunately nullability doesn't flow :(
        return new Address(country!, zipCode!, city!, street!);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return ZipCode;
        yield return City;
        yield return Street;
    }
}
