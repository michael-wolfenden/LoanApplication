using LoanApplication.Tests.Helpers;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class LoanAmountTests
{
    [Fact]
    public void loan_amount_must_be_positive() =>
        LoanAmount.Of(-1).ShouldFailWithMessage("LoanAmount must be greater than zero.");

    [Fact]
    public void can_parse_valid_load_amount() =>
        LoanAmount.Of(1234).ShouldSucceed();
}
