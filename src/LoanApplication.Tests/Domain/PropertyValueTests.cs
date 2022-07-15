using LoanApplication.Tests.Helpers;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class PropertyValueTests
{
    [Fact]
    public void property_value_must_be_positive() =>
        PropertyValue.Of(-1).ShouldFailWithMessage("PropertyValue must be greater than zero.");

    [Fact]
    public void can_parse_valid_load_amount() =>
        PropertyValue.Of(1234).ShouldSucceed();
}
