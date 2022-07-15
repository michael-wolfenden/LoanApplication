using LoanApplication.Tests.Helpers;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class PasswordTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void password_is_required(string nullOrWhitespace) =>
        Password.Of(nullOrWhitespace)
            .ShouldFailWithMessage("Password cannot be empty or consist only of white-space characters.");

    [Fact]
    public void password_has_max_length_50() =>
        Password.Of(new string('*', 51))
            .ShouldFailWithMessage("Password cannot be longer than 50 characters.");

    [Fact]
    public void can_parse_valid_password() =>
        Password.Of(new string('*', 50)).ShouldSucceed();
}
