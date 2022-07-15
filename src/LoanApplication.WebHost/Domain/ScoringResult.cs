using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class ScoringResult : ValueObject
{
    private ScoringResult(ApplicationScore? score, string? explanation)
    {
        Score = score;
        Explanation = explanation;
    }

    public ApplicationScore? Score { get; }
    public string? Explanation { get; }

    public static ScoringResult Green() =>
        new(ApplicationScore.Green, null);

    public static ScoringResult Red(string[] messages) =>
        new(ApplicationScore.Red, string.Join(Environment.NewLine, messages));

    public bool IsRed() =>
        Score == ApplicationScore.Red;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Score!;
        yield return Explanation!;
    }
}
