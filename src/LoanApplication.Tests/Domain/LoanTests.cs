using FluentAssertions;
using LoanApplication.Tests.Builders;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Domain;

using static LoanBuilder;

public class LoanTests
{
    [Fact]
    public void Can_calculate_monthly_installment()
    {
        var loan = GivenLoan()
            .WithLoanAmount(420_000M)
            .WithLoanNumberOfYears(3)
            .WithInterestRate(5M)
            .Build();

        var installment = loan.MonthlyInstallment();

        installment.Should().Be(new MonetaryAmount(12_587.78M));
    }
}
