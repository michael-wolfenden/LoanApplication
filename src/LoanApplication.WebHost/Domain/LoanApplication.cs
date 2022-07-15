using CSharpFunctionalExtensions;
using LoanApplication.WebHost.Domain.Scoring;

namespace LoanApplication.WebHost.Domain;

public class LoanApplication : Entity<LoanApplicationId>
{
    private LoanApplication(
        LoanApplicationNumber number,
        LoanApplicationStatus status,
        Customer customer,
        Property property,
        Loan loan,
        ScoringResult? score,
        Registration registration,
        Decision? decision) : base(LoanApplicationId.New())
    {
        Number = number;
        Status = status;
        Score = score;
        Customer = customer;
        Property = property;
        Loan = loan;
        Registration = registration;
        Decision = decision;
    }

    public LoanApplication(
        LoanApplicationNumber number,
        Customer customer,
        Property property,
        Loan loan,
        Operator registeredBy,
        DateOnly today)
        : this(number, LoanApplicationStatus.New, customer, property, loan, null, new Registration(today, registeredBy), null)
    {
    }

    //To satisfy EF Core, disable non nullable checks
#pragma warning disable CS8618
    protected LoanApplication()
#pragma warning restore CS8618
    {
    }

    public LoanApplicationStatus Status { get; private set; }
    public Decision? Decision { get; private set; }
    public LoanApplicationNumber Number { get; }
    public Customer Customer { get; }
    public Property Property { get; }
    public Loan Loan { get; }
    public Registration Registration { get; }
    public ScoringResult? Score { get; private set; }

    public void Evaluate(ScoringRules rules)
    {
        Score = rules.Evaluate(this);
        if (Score.IsRed()) Status = LoanApplicationStatus.Rejected;
    }

    public UnitResult<Error> Accept(Operator decisionBy, DateOnly today)
    {
        if (Status != LoanApplicationStatus.New)
            return Errors.CannotAcceptApplicationThatIsAcceptedOrRejected();

        if (Score is null)
            return Errors.CannotAcceptApplicationBeforeScoring();

        if (!decisionBy.CanAccept(Loan.LoanAmount))
            return Errors.OperatorDoesNotHaveCompetenceLevel();

        Status = LoanApplicationStatus.Accepted;
        Decision = new Decision(today, decisionBy);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> Reject(Operator decisionBy, DateOnly today)
    {
        if (Status != LoanApplicationStatus.New)
            return Errors.CannotRejectApplicationThatIsAcceptedOrRejected();

        Status = LoanApplicationStatus.Rejected;
        Decision = new Decision(today, decisionBy);

        return UnitResult.Success<Error>();
    }
}
