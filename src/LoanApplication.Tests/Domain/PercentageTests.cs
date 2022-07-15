using LoanApplication.Tests.Helpers;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class PercentTests
{
    [Fact]
    public void percent_must_be_positive() =>
        Percent.Of(-1).ShouldFailWithMessage("Percent must be greater than zero.");

    [Fact]
    public void can_parse_valid_load_amount() =>
        Percent.Of(1234).ShouldSucceed();
}
