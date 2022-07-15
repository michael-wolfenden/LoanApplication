using CSharpFunctionalExtensions;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Builders;

public class AddressBuilder
{
    private string? _city;
    private string? _country;
    private string? _street;
    private string? _zipCode;

    public AddressBuilder()
    {
        WithCity("San Jose");
        WithZipCode("95-118");
        WithStreet("2201 Sycamore Street");
        WithCountry("CA");
    }

    public static AddressBuilder GivenAddress() =>
        new();

    public AddressBuilder WithCountry(string country) =>
        Set(x => x._country = country);

    public AddressBuilder WithZipCode(string zipCode) =>
        Set(x => x._zipCode = zipCode);

    public AddressBuilder WithStreet(string street) =>
        Set(x => x._street = street);

    public AddressBuilder WithCity(string city) =>
        Set(x => x._city = city);

    private AddressBuilder Set(Action<AddressBuilder> setter)
    {
        setter(this);
        return this;
    }

    public Result<Address, Error> Build() =>
        Address.Of(_country, _zipCode, _city, _street);
}
