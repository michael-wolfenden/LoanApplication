using CSharpFunctionalExtensions;
using MediatR;

namespace LoanApplication.WebHost.Domain.UseCases;

public class SubmitLoanApplication
{
    public record Request
    (
        string CustomerNationalIdentifier,
        Request.Name CustomerName,
        DateTime CustomerBirthdate,
        decimal CustomerMonthlyIncome,
        Request.Address CustomerAddress,
        decimal PropertyValue,
        Request.Address PropertyAddress,
        decimal LoanAmount,
        int LoanNumberOfYears,
        decimal InterestRate
    ) : IRequest<Result<Response, Error>>
    {
        public record Name
        (
            string First,
            string Last
        );

        public record Address
        (
            string Country,
            string ZipCode,
            string City,
            string Street
        );
    }

    public record Response(string LoanApplicationNumber);

    public class Handler : IRequestHandler<Request, Result<Response, Error>>
    {
        private readonly ICurrentDateProvider _currentDateProvider;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILoanApplications _loanApplications;
        private readonly IOperators _operators;

        public Handler(ILoanApplications loanApplications, IOperators operators, ICurrentUserProvider currentUserProvider,
            ICurrentDateProvider currentDateProvider)
        {
            _loanApplications = loanApplications;
            _operators = operators;
            _currentUserProvider = currentUserProvider;
            _currentDateProvider = currentDateProvider;
        }

        public async Task<Result<Response, Error>> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = (
                NationalIdentifier.Of(request.CustomerNationalIdentifier, nameof(request.CustomerNationalIdentifier)),
                Name.Of(request.CustomerName.First, request.CustomerName.Last, nameof(request.CustomerName)),
                Address.Of(
                    request.CustomerAddress.Country,
                    request.CustomerAddress.ZipCode,
                    request.CustomerAddress.City,
                    request.CustomerAddress.Street,
                    nameof(request.CustomerAddress)),
                Address.Of(
                    request.PropertyAddress.Country,
                    request.PropertyAddress.ZipCode,
                    request.PropertyAddress.City,
                    request.PropertyAddress.Street,
                    nameof(request.PropertyAddress)),
                Percent.Of(request.InterestRate, nameof(request.InterestRate)),
                PropertyValue.Of(request.PropertyValue),
                LoanAmount.Of(request.LoanAmount),
                LoanNumberOfYears.Of(request.LoanNumberOfYears, "ASdsad"),
                Login.Of(_currentUserProvider.Username)
            ).Combine();

            if (result.IsFailure) return result.Error;

            var (
                customerNationalIdentifier,
                customerName,
                customerAddress,
                propertyAddress,
                interestRate,
                propertyValue,
                loanAmount,
                loanNumberOfYears,
                login
                ) = result.Value;

            var user = await _operators.WithLogin(login);
            if (user is null) return Errors.NotFound("Operator", login);

            var application = new LoanApplication
            (
                LoanApplicationNumber.New(),
                new Customer
                (
                    customerNationalIdentifier,
                    customerName,
                    DateOnly.FromDateTime(request.CustomerBirthdate),
                    new MonetaryAmount(request.CustomerMonthlyIncome),
                    customerAddress
                ),
                new Property(propertyValue, propertyAddress),
                new Loan(loanAmount, loanNumberOfYears, interestRate),
                user,
                _currentDateProvider.Today
            );

            await _loanApplications.Add(application);

            return new Response(application.Number);
        }
    }
}
