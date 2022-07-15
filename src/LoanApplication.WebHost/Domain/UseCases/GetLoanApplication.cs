using CSharpFunctionalExtensions;
using MediatR;

namespace LoanApplication.WebHost.Domain.UseCases;

public class GetLoanApplication
{
    public record Request(string ApplicationNumber) : IRequest<Result<Response, Error>>
    {
    }

    public record Response(
        string Number,
        string Status,
        string? Score,
        string? Explanation,
        string CustomerNationalIdentifier,
        string CustomerFirstName,
        string CustomerLastName,
        DateTime CustomerBirthdate,
        decimal CustomerMonthlyIncome,
        Response.Address CustomerAddress,
        decimal PropertyValue,
        Response.Address PropertyAddress,
        decimal LoanAmount,
        int LoanNumberOfYears,
        decimal InterestRate,
        DateTime? DecisionDate,
        string? DecisionBy,
        string RegisteredBy,
        DateTime RegistrationDate
    )
    {
        public record Address
        (
            string Country,
            string ZipCode,
            string City,
            string Street
        );
    }

    public class Handler : IRequestHandler<Request, Result<Response, Error>>
    {
        private readonly ILoanApplications _loanApplications;

        public Handler(ILoanApplications loanApplications) =>
            _loanApplications = loanApplications;

        public async Task<Result<Response, Error>> Handle(Request request, CancellationToken cancellationToken)
        {
            var loanApplicationNumber = LoanApplicationNumber.Of(request.ApplicationNumber);
            if (loanApplicationNumber.IsFailure) return loanApplicationNumber.Error;

            var loanApplication = await _loanApplications.WithNumber(loanApplicationNumber.Value);
            if (loanApplication is null) return Errors.NotFound(nameof(LoanApplication), loanApplicationNumber.Value);

            var customer = loanApplication.Customer;
            var property = loanApplication.Property;

            return new Response(
                loanApplication.Number,
                loanApplication.Status.ToString(),
                loanApplication.Score?.Score?.ToString(),
                loanApplication.Score?.Explanation,
                customer.NationalIdentifier,
                customer.Name.First,
                customer.Name.Last,
                customer.Birthdate.ToDateTime(TimeOnly.MinValue),
                customer.MonthlyIncome,
                new Response.Address(customer.Address.Country, customer.Address.ZipCode, customer.Address.City, customer.Address.Street),
                property.Value,
                new Response.Address(property.Address.Country, property.Address.ZipCode, property.Address.City, property.Address.Street),
                loanApplication.Loan.LoanAmount,
                loanApplication.Loan.LoanNumberOfYears,
                loanApplication.Loan.InterestRate,
                loanApplication.Decision?.DecisionDate.ToDateTime(TimeOnly.MinValue),
                loanApplication.Decision?.DecisionBy.Value.ToString(),
                loanApplication.Registration.RegisteredBy.Value.ToString(),
                loanApplication.Registration.RegistrationDate.ToDateTime(TimeOnly.MinValue));
        }
    }
}
