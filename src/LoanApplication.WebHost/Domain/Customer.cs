using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Customer : ValueObject
{
    public Customer
    (
        NationalIdentifier nationalIdentifier,
        Name name,
        DateOnly birthdate,
        MonetaryAmount monthlyIncome,
        Address address
    )
    {
        NationalIdentifier = nationalIdentifier;
        Name = name;
        Birthdate = birthdate;
        MonthlyIncome = monthlyIncome;
        Address = address;
    }

    //To satisfy EF Core, disable non nullable checks
#pragma warning disable CS8618
    protected Customer()
#pragma warning restore CS8618
    {
    }

    public NationalIdentifier NationalIdentifier { get; }
    public Name Name { get; }
    public DateOnly Birthdate { get; }
    public MonetaryAmount MonthlyIncome { get; }
    public Address Address { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return NationalIdentifier;
        yield return Name;
        yield return Birthdate;
        yield return MonthlyIncome;
        yield return Address;
    }

    public Age AgeAt(DateOnly date) =>
        Age.Between(Birthdate, date);
}
