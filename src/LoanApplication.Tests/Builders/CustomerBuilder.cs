using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Builders;

public class CustomerBuilder
{
    private readonly DateOnly _today;

    private Address? _address;
    private DateOnly _birthdate;
    private MonetaryAmount? _monthlyIncome;
    private Name? _name;
    private NationalIdentifier? _nationalIdentifier;

    public CustomerBuilder(DateOnly today)
    {
        _today = today;

        WithName("Jan", "B");
        WithNationalIdentifier("11111111111");
        WithMonthlyIncome(15_500M);
        WithAddress();
        WithAge(25);
    }

    public static CustomerBuilder GivenCustomer(DateOnly? today = null) =>
        new(today ?? new DateOnly(2000, 01, 01));

    public CustomerBuilder WithNationalIdentifier(string nationalIdentifier) =>
        Set(x => x._nationalIdentifier = NationalIdentifier.Of(nationalIdentifier).Value);

    public CustomerBuilder WithName(string first, string last) =>
        Set(x => x._name = Name.Of(first, last).Value);

    public CustomerBuilder BornOn(DateOnly birthdate) =>
        Set(x => x._birthdate = birthdate);

    public CustomerBuilder WithAge(int age) =>
        BornOn(_today.AddYears(-1 * age));

    public CustomerBuilder WithMonthlyIncome(decimal monthlyIncome) =>
        Set(x => x._monthlyIncome = new MonetaryAmount(monthlyIncome));

    public CustomerBuilder WithAddress(Action<AddressBuilder>? customizeAddress = null)
    {
        var addressBuilder = new AddressBuilder();
        customizeAddress?.Invoke(addressBuilder);

        return Set(x => x._address = addressBuilder.Build().Value);
    }

    private CustomerBuilder Set(Action<CustomerBuilder> setter)
    {
        setter(this);
        return this;
    }

    public Customer Build() =>
        new(_nationalIdentifier!, _name!, _birthdate!, _monthlyIncome!, _address!);
}
