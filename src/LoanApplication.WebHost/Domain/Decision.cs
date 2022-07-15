using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Decision : ValueObject
{
    public Decision(DateOnly decisionDate, Operator decisionBy)
    {
        DecisionDate = decisionDate;
        DecisionBy = decisionBy.Id;
    }

    //To satisfy EF Core, disable non nullable checks
#pragma warning disable CS8618
    protected Decision()
#pragma warning restore CS8618
    {
    }

    public DateOnly DecisionDate { get; }
    public OperatorId DecisionBy { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DecisionDate;
        yield return DecisionBy;
    }
}
