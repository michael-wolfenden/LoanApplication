using CSharpFunctionalExtensions;
using MediatR;

namespace LoanApplication.WebHost.Domain.UseCases;

public class RejectLoanApplication
{
    public record Request(string ApplicationNumber) : IRequest<Result<Unit, Error>>
    {
    }

    public class Handler : IRequestHandler<Request, Result<Unit, Error>>
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

        public async Task<Result<Unit, Error>> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = (
                LoanApplicationNumber.Of(request.ApplicationNumber),
                Login.Of(_currentUserProvider.Username)
            ).Combine();

            if (result.IsFailure) return result.Error;

            var (loanApplicationNumber, login) = result.Value;

            var loanApplication = await _loanApplications.WithNumber(loanApplicationNumber);
            if (loanApplication is null) return Errors.NotFound(nameof(LoanApplication), loanApplicationNumber);

            var user = await _operators.WithLogin(login);
            if (user is null) return Errors.NotFound(nameof(Operator), login);

            var acceptResult = loanApplication.Reject(user, _currentDateProvider.Today);
            if (acceptResult.IsFailure) return acceptResult.Error;

            return Unit.Value;
        }
    }
}
