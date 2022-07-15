namespace LoanApplication.WebHost.Domain.Scoring;

public class ScoringRules
{
    private readonly IEnumerable<IScoringRule> _rules;

    public ScoringRules(IEnumerable<IScoringRule> rules) =>
        _rules = rules;

    public ScoringResult Evaluate(LoanApplication loanApplication)
    {
        var brokenRules = _rules
            .Where(r => !r.IsSatisfiedBy(loanApplication))
            .ToList();

        return brokenRules.Any() ? ScoringResult.Red(brokenRules.Select(r => r.Message).ToArray()) : ScoringResult.Green();
    }
}
