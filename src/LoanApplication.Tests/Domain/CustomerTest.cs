using FluentAssertions;
using LoanApplication.WebHost.Domain;
using static LoanApplication.Tests.Builders.CustomerBuilder;

namespace LoanApplication.Tests.Domain;

public class CustomerTest
{
    [Fact]
    public void is_45_in_2019_if_born_in_1974()
    {
        var customer = GivenCustomer()
            .BornOn(new DateOnly(1974, 6, 26))
            .Build();

        var ageAt2019 = customer.AgeAt(new DateOnly(2019, 1, 1));

        ageAt2019.Should().Be(45.Years());
    }

    [Fact]
    public void is_46_in_2020_if_born_in_1974()
    {
        var customer = GivenCustomer()
            .BornOn(new DateOnly(1974, 6, 26))
            .Build();

        var ageAt2020 = customer.AgeAt(new DateOnly(2020, 1, 1));

        ageAt2020.Should().Be(46.Years());
    }

    [Fact]
    public void is_47_in_2021_if_born_in_1974()
    {
        var customer = GivenCustomer()
            .BornOn(new DateOnly(1974, 6, 26))
            .Build();

        var ageAt2021 = customer.AgeAt(new DateOnly(2021, 1, 1));

        ageAt2021.Should().Be(47.Years());
    }
}
