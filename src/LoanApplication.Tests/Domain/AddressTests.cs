using LoanApplication.Tests.Helpers;
using static LoanApplication.Tests.Builders.AddressBuilder;

namespace LoanApplication.Tests.Domain;

public class AddressTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void country_is_required(string nullOrWhitespace) =>
        GivenAddress()
            .WithCountry(nullOrWhitespace)
            .Build()
            .ShouldFailWithMessage("Country cannot be empty or consist only of white-space characters.");

    [Fact]
    public void country_has_maxlength_of_50() =>
        GivenAddress()
            .WithCountry(new string('*', 51))
            .Build()
            .ShouldFailWithMessage("Country cannot be longer than 50 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void street_is_required(string nullOrWhitespace) =>
        GivenAddress()
            .WithStreet(nullOrWhitespace)
            .Build()
            .ShouldFailWithMessage("Street cannot be empty or consist only of white-space characters.");

    [Fact]
    public void street_has_maxlength_of_50() =>
        GivenAddress()
            .WithStreet(new string('*', 51))
            .Build()
            .ShouldFailWithMessage("Street cannot be longer than 50 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void city_is_required(string nullOrWhitespace) =>
        GivenAddress()
            .WithCity(nullOrWhitespace)
            .Build()
            .ShouldFailWithMessage("City cannot be empty or consist only of white-space characters.");

    [Fact]
    public void city_has_maxlength_of_50() =>
        GivenAddress()
            .WithCity(new string('*', 51))
            .Build()
            .ShouldFailWithMessage("City cannot be longer than 50 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void zip_code_is_required(string nullOrWhitespace) =>
        GivenAddress()
            .WithZipCode(nullOrWhitespace)
            .Build()
            .ShouldFailWithMessage("ZipCode cannot be empty or consist only of white-space characters.");

    [Fact]
    public void zip_code_must_be_2_numbers_followed_by_dash_then_3_numbers() =>
        GivenAddress()
            .WithZipCode("ABCDEF")
            .Build()
            .ShouldFailWithMessage("ZipCode did not match pattern [0-9]{2}-[0-9]{3}.");

    [Fact]
    public void can_parse_a_valid_address()
    {
        var stringLength50 = new string('*', 50);

        GivenAddress()
            .WithCity(stringLength50)
            .WithZipCode("12-345")
            .WithStreet(stringLength50)
            .WithCountry(stringLength50)
            .Build()
            .ShouldSucceed();
    }
}
