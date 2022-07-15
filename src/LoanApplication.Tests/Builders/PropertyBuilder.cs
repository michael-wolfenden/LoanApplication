using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Builders;

public class PropertyBuilder
{
    private Address? _address;
    private PropertyValue? _value;

    public PropertyBuilder()
    {
        WithValue(400_000M);
        WithAddress();
    }

    public static PropertyBuilder GivenProperty() =>
        new();

    public PropertyBuilder WithValue(decimal propertyValue) =>
        Set(x => x._value = PropertyValue.Of(propertyValue).Value);

    public PropertyBuilder WithAddress(Action<AddressBuilder>? customizeAddress = null)
    {
        var addressBuilder = new AddressBuilder();
        customizeAddress?.Invoke(addressBuilder);

        return Set(x => x._address = addressBuilder.Build().Value);
    }

    private PropertyBuilder Set(Action<PropertyBuilder> setter)
    {
        setter(this);
        return this;
    }

    public Property Build() =>
        new(_value!, _address!);
}
