using CSharpFunctionalExtensions;
using LoanApplication.WebHost.Domain.Scoring;
using MediatR;

namespace LoanApplication.WebHost.Domain.UseCases;

public class EvaluateLoanApplication
{
    public record Request(string ApplicationNumber) : IRequest<Result<Unit, Error>>
    {
    }

    public class Handler : IRequestHandler<Request, Result<Unit, Error>>
    {
        private readonly ICurrentDateProvider _currentDateProvider;
        private readonly IDebtorRegistry _debtorRegistry;
        private readonly ILoanApplications _loanApplications;

        public Handler(ILoanApplications loanApplications, ICurrentDateProvider currentDateProvider, IDebtorRegistry debtorRegistry)
        {
            _loanApplications = loanApplications;
            _currentDateProvider = currentDateProvider;
            _debtorRegistry = debtorRegistry;
        }

        public async Task<Result<Unit, Error>> Handle(Request request, CancellationToken cancellationToken)
        {
            var loanApplicationNumber = LoanApplicationNumber.Of(request.ApplicationNumber);
            if (loanApplicationNumber.IsFailure) return loanApplicationNumber.Error;

            var loanApplication = await _loanApplications.WithNumber(loanApplicationNumber.Value);
            if (loanApplication is null) return Errors.NotFound(nameof(LoanApplication), loanApplicationNumber.Value);

            var scoringRulesFactory = new ScoringRulesFactory(_currentDateProvider.Today, _debtorRegistry);
            loanApplication.Evaluate(scoringRulesFactory.DefaultSet);

            return Unit.Value;
        }
    }
}
