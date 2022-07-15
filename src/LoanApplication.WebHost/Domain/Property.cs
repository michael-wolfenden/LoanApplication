using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Property : ValueObject
{
    public Property(PropertyValue value, Address address)
    {
        Value = value;
        Address = address;
    }

    //To satisfy EF Core, disable non nullable checks
#pragma warning disable CS8618
    protected Property()
#pragma warning restore CS8618
    {
    }

    public PropertyValue Value { get; }
    public Address Address { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Address;
    }
}
