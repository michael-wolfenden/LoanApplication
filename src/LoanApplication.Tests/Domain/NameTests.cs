using LoanApplication.Tests.Helpers;
using static LoanApplication.Tests.Builders.NameBuilder;

namespace LoanApplication.Tests.Domain;

public class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void first_name_is_required(string nullOrWhitespace) =>
        GivenName()
            .WithFirst(nullOrWhitespace)
            .Build()
            .ShouldFailWithMessage("First cannot be empty or consist only of white-space characters.");

    [Fact]
    public void first_name_has_maxlength_of_50() =>
        GivenName()
            .WithFirst(new string('*', 51))
            .Build()
            .ShouldFailWithMessage("First cannot be longer than 50 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void last_name_is_required(string nullOrWhitespace) =>
        GivenName()
            .WithLast(nullOrWhitespace)
            .Build()
            .ShouldFailWithMessage("Last cannot be empty or consist only of white-space characters.");

    [Fact]
    public void last_name_has_maxlength_of_75() =>
        GivenName()
            .WithLast(new string('*', 76))
            .Build()
            .ShouldFailWithMessage("Last cannot be longer than 75 characters.");

    [Fact]
    public void can_parse_a_valid_name() =>
        GivenName()
            .WithFirst(new string('*', 50))
            .WithLast(new string('*', 75))
            .Build()
            .ShouldSucceed();
}
