using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Operator : Entity<OperatorId>
{
    public Operator(Login login, Password password, Name name, MonetaryAmount competenceLevel) : base(OperatorId.New())
    {
        Login = login;
        Password = password;
        Name = name;
        CompetenceLevel = competenceLevel;
    }

    //To satisfy EF Core, disable non nullable checks
#pragma warning disable CS8618
    protected Operator()
#pragma warning restore CS8618
    {
    }

    public Login Login { get; }
    public Password Password { get; }
    public Name Name { get; }
    public MonetaryAmount CompetenceLevel { get; }

    public bool CanAccept(LoanAmount loanAmount) =>
        loanAmount <= CompetenceLevel;
}
