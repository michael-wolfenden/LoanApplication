using FluentAssertions;
using LoanApplication.WebHost.Domain;
using LoanApplication.WebHost.Domain.Scoring;
using LoanApplication.WebHost.Domain.Scoring.Rules;
using static LoanApplication.Tests.Builders.LoanApplicationBuilder;

namespace LoanApplication.Tests.Domain.Scoring;

public class ScoringRulesTests
{
    [Fact]
    public void satisfied_when_loan_amount_is_less_than_property_value()
    {
        var application = GivenLoanApplication()
            .WithProperty(prop => prop.WithValue(750_000M))
            .WithLoan(loan => loan.WithLoanAmount(300_000M))
            .Build();

        var rule = new LoanAmountMustBeLowerThanPropertyValue();
        var ruleCheckResult = rule.IsSatisfiedBy(application);

        ruleCheckResult.Should().BeTrue();
    }

    [Fact]
    public void non_satisfied_when_loan_amount_is_greater_than_property_value()
    {
        var application = GivenLoanApplication()
            .WithProperty(property => property.WithValue(750_000M))
            .WithLoan(loan => loan.WithLoanAmount(800_000M))
            .Build();

        var rule = new LoanAmountMustBeLowerThanPropertyValue();
        var ruleCheckResult = rule.IsSatisfiedBy(application);

        ruleCheckResult.Should().BeFalse();
    }

    [Fact]
    public void satisfied_when_customer_not_older_than_65_at_the_date_of_the_last_installment()
    {
        var today = new DateOnly(2000, 01, 01);

        var application = GivenLoanApplication(today)
            .WithLoan(loan => loan.WithLoanNumberOfYears(20))
            .WithCustomer(customer => customer.WithAge(26))
            .Build();

        var rule = new CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65(today);
        var ruleCheckResult = rule.IsSatisfiedBy(application);

        ruleCheckResult.Should().BeTrue();
    }

    [Fact]
    public void not_satisfied_when_customer_is_older_than_65_at_the_date_of_the_last_installment()
    {
        var today = new DateOnly(2000, 01, 01);

        var application = GivenLoanApplication(today)
            .WithLoan(loan => loan.WithLoanNumberOfYears(20))
            .WithCustomer(customer => customer.WithAge(46))
            .Build();

        var rule = new CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65(today);
        var ruleCheckResult = rule.IsSatisfiedBy(application);

        ruleCheckResult.Should().BeFalse();
    }

    [Fact]
    public void satisfied_when_instalment_amount_lower_than_15_percent_of_customer_income()
    {
        var application = GivenLoanApplication()
            .WithLoan(loan => loan
                .WithLoanNumberOfYears(25)
                .WithLoanAmount(400_000M)
                .WithInterestRate(1M))
            .WithCustomer(customer => customer
                .WithMonthlyIncome(11_000M))
            .Build();

        var rule = new InstallmentAmountMustBeLowerThen15PercentOfCustomerIncome();
        var ruleCheckResult = rule.IsSatisfiedBy(application);

        ruleCheckResult.Should().BeTrue();
    }

    [Fact]
    public void not_satisfied_when_instalment_amount_greater_than_15_percent_of_customer_income()
    {
        var application = GivenLoanApplication()
            .WithLoan(loan => loan
                .WithLoanNumberOfYears(20)
                .WithLoanAmount(400_000M)
                .WithInterestRate(1M))
            .WithCustomer(customer => customer
                .WithMonthlyIncome(4_000M))
            .Build();

        var rule = new InstallmentAmountMustBeLowerThen15PercentOfCustomerIncome();
        var ruleCheckResult = rule.IsSatisfiedBy(application);

        ruleCheckResult.Should().BeFalse();
    }

    [Fact]
    public void satisfied_when_customer_is_not_a_registered_debtor()
    {
        var debtorRegistry = new DebtorRegistryStub("NOT_OUR_CUSTOMER_IDENTIFIER");

        var application = GivenLoanApplication()
            .WithCustomer(customer => customer.WithNationalIdentifier("71041864667"))
            .Build();

        var rule = new CustomerIsNotARegisteredDebtor(debtorRegistry);
        var ruleCheckResult = rule.IsSatisfiedBy(application);

        ruleCheckResult.Should().BeTrue();
    }

    [Fact]
    public void not_satisfied_when_customer_is_a_registered_debtor()
    {
        var debtorRegistry = new DebtorRegistryStub("71041864667");

        var application = GivenLoanApplication()
            .WithCustomer(customer => customer.WithNationalIdentifier("71041864667"))
            .Build();

        var rule = new CustomerIsNotARegisteredDebtor(debtorRegistry);
        var ruleCheckResult = rule.IsSatisfiedBy(application);

        ruleCheckResult.Should().BeFalse();
    }

    [Fact]
    public void scoring_result_is_red_when_any_rule_is_not_satisfied()
    {
        var today = new DateOnly(2000, 01, 01);
        var debtorRegistry = new DebtorRegistryStub("71041864667");

        var application = GivenLoanApplication(today)
            .WithLoan(loan => loan
                .WithLoanNumberOfYears(20)
                .WithLoanAmount(400_000M)
                .WithInterestRate(1M))
            .WithCustomer(customer => customer
                .WithNationalIdentifier("71041864667")
                .WithMonthlyIncome(4_000M))
            .Build();

        var scoringRulesFactory = new ScoringRulesFactory(today, debtorRegistry);
        var score = scoringRulesFactory.DefaultSet.Evaluate(application);

        score.Score.Should().Be(ApplicationScore.Red);
        score.Explanation.Should().Be(@"
Property value is lower than loan amount.
Installment is higher than 15% of customer's income.
Customer is registered in debtor registry
".Trim());
    }

    [Fact]
    public void scoring_result_is_green_when_all_rules_are_satisfied()
    {
        var today = new DateOnly(2000, 01, 01);
        var debtorRegistry = new DebtorRegistryStub("NOT_OUR_CUSTOMER_IDENTIFIER");

        var application = GivenLoanApplication(today)
            .WithCustomer(customer => customer
                .WithNationalIdentifier("71041864667")
                .WithAge(25)
                .WithMonthlyIncome(15_000M))
            .WithLoan(loan => loan
                .WithLoanAmount(200_000)
                .WithLoanNumberOfYears(25)
                .WithInterestRate(1.1M))
            .WithProperty(prop => prop.WithValue(250_000M))
            .Build();

        var scoringRulesFactory = new ScoringRulesFactory(today, debtorRegistry);
        var score = scoringRulesFactory.DefaultSet.Evaluate(application);

        score.Score.Should().Be(ApplicationScore.Green);
        score.Explanation.Should().BeNull();
    }

    private class DebtorRegistryStub : IDebtorRegistry
    {
        private readonly string _registerDebtorIdentifier;

        public DebtorRegistryStub(string registerDebtorIdentifier = "11111111111") =>
            _registerDebtorIdentifier = registerDebtorIdentifier;

        public bool IsRegisteredDebtor(Customer customer) =>
            customer.NationalIdentifier == _registerDebtorIdentifier;
    }
}
