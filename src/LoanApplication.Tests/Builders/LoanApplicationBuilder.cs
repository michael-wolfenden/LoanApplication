using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Builders;

public class LoanApplicationBuilder
{
    private readonly DateOnly _today;
    private Customer? _customer;
    private Loan? _loan;
    private LoanApplicationNumber? _number;
    private Property? _property;
    private Operator? _registeredBy;

    public LoanApplicationBuilder(DateOnly today)
    {
        _today = today;

        WithNumber(new Guid().ToString());
        WithCustomer();
        WithProperty();
        WithLoan();
        WithRegisteredBy();
    }

    public static LoanApplicationBuilder GivenLoanApplication(DateOnly? today = null) =>
        new(today ?? new DateOnly(2000, 01, 01));

    public LoanApplicationBuilder WithNumber(string number) =>
        Set(x => x._number = LoanApplicationNumber.Of(number).Value);

    public LoanApplicationBuilder WithCustomer(Action<CustomerBuilder>? customizeCustomer = null)
    {
        var customerBuilder = new CustomerBuilder(_today);
        customizeCustomer?.Invoke(customerBuilder);

        Set(x => x._customer = customerBuilder.Build());

        return this;
    }

    public LoanApplicationBuilder WithProperty(Action<PropertyBuilder>? customizeProperty = null)
    {
        var propertyBuilder = new PropertyBuilder();
        customizeProperty?.Invoke(propertyBuilder);

        return Set(x => x._property = propertyBuilder.Build());
    }

    public LoanApplicationBuilder WithLoan(Action<LoanBuilder>? customizeLoan = null)
    {
        var loanBuilder = new LoanBuilder();
        customizeLoan?.Invoke(loanBuilder);

        return Set(x => x._loan = loanBuilder.Build());
    }

    public LoanApplicationBuilder WithRegisteredBy(Action<OperatorBuilder>? customizeOperator = null)
    {
        var operatorBuilder = new OperatorBuilder();
        customizeOperator?.Invoke(operatorBuilder);

        return Set(x => x._registeredBy = operatorBuilder.Build());
    }

    private LoanApplicationBuilder Set(Action<LoanApplicationBuilder> setter)
    {
        setter(this);
        return this;
    }

    public WebHost.Domain.LoanApplication Build() =>
        new(_number!, _customer!, _property!, _loan!, _registeredBy!, _today!);
}
