using FluentAssertions;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

public class MonetaryAmountTests
{
    [Fact]
    public void same_amounts_are_equal()
    {
        var one = new MonetaryAmount(10M);
        var two = new MonetaryAmount(10M);

        one.Equals(two).Should().BeTrue();
        (one == two).Should().BeTrue();
    }

    [Fact]
    public void different_amounts_are_not_equal()
    {
        var one = new MonetaryAmount(10M);
        var two = new MonetaryAmount(11M);

        one.Equals(two).Should().BeFalse();
        (one != two).Should().BeTrue();
    }

    [Fact]
    public void can_be_compared()
    {
        var one = new MonetaryAmount(10M);
        var two = new MonetaryAmount(11M);

        (one < two).Should().BeTrue();
        (one > two).Should().BeFalse();
    }

    [Fact]
    public void can_be_added()
    {
        var one = new MonetaryAmount(10M);
        var two = new MonetaryAmount(11M);

        var sum = one + two;

        sum.Should().Be(new MonetaryAmount(21M));
    }

    [Fact]
    public void can_be_subtracted()
    {
        var one = new MonetaryAmount(10M);
        var two = new MonetaryAmount(5M);

        var diff = one - two;

        diff.Should().Be(new MonetaryAmount(5M));
    }

    [Fact]
    public void can_multiply_by_percent()
    {
        var amount = new MonetaryAmount(10M) * Percent.Fifteen;

        amount.Should().Be(new MonetaryAmount(1.5M));
    }
}
