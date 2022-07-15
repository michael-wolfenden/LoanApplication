using LoanApplication.Tests.Helpers;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class LoginTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void login_number_is_required(string nullOrWhitespace) =>
        Login.Of(nullOrWhitespace)
            .ShouldFailWithMessage("Login cannot be empty or consist only of white-space characters.");

    [Fact]
    public void login_number_has_max_length_50() =>
        Login.Of(new string('*', 51))
            .ShouldFailWithMessage("Login cannot be longer than 50 characters.");

    [Fact]
    public void can_parse_valid_login_number() =>
        Login.Of(new string('*', 50)).ShouldSucceed();
}
