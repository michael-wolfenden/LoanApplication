using LoanApplication.Tests.Helpers;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class NationalIdentifierTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void national_identifier_is_required(string nullOrWhitespace) =>
        NationalIdentifier.Of(nullOrWhitespace)
            .ShouldFailWithMessage("NationalIdentifier cannot be empty or consist only of white-space characters.");

    [Theory]
    [InlineData("1234567890")]
    [InlineData("123456789123")]
    public void national_identifier_must_be_11_characters(string not11Characters) =>
        NationalIdentifier.Of(not11Characters)
            .ShouldFailWithMessage("NationalIdentifier must consist of 11 characters.");

    [Fact]
    public void can_parse_valid_national_identifier() =>
        NationalIdentifier.Of(new string('*', 11)).ShouldSucceed();
}
