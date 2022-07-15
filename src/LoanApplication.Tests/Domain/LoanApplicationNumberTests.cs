using LoanApplication.Tests.Helpers;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class LoanApplicationNumberTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void loan_application_number_is_required(string nullOrWhitespace) =>
        LoanApplicationNumber.Of(nullOrWhitespace)
            .ShouldFailWithMessage("LoanApplicationNumber cannot be empty or consist only of white-space characters.");

    [Fact]
    public void loan_application_number_has_max_length_50() =>
        LoanApplicationNumber.Of(new string('*', 51))
            .ShouldFailWithMessage("LoanApplicationNumber cannot be longer than 50 characters.");

    [Fact]
    public void can_parse_valid_loan_application_number() =>
        LoanApplicationNumber.Of(new string('*', 50)).ShouldSucceed();
}
