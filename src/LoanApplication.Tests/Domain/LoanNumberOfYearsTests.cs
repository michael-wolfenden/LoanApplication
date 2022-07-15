using LoanApplication.Tests.Helpers;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class LoanNumberOfYearsTests
{
    [Fact]
    public void loan_number_of_years_must_be_positive() =>
        LoanNumberOfYears.Of(-1).ShouldFailWithMessage("LoanNumberOfYears must be greater than zero.");

    [Fact]
    public void can_parse_valid_load_amount() =>
        LoanNumberOfYears.Of(1234).ShouldSucceed();
}
