using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Builders;

public class OperatorBuilder
{
    private MonetaryAmount? _competenceLevel;
    private Login? _login;
    private Name? _name;
    private Password? _password;

    public OperatorBuilder()
    {
        WithLogin("admin");
        WithCompetenceLevel(1_000_000M);
    }

    public static OperatorBuilder GivenOperator() =>
        new();

    public OperatorBuilder WithLogin(string login) =>
        Set(x => x._login = Login.Of(login).Value)
            .Set(x => x._password = Password.Of(login).Value)
            .Set(x => x._name = Name.Of(login, login).Value);

    public OperatorBuilder WithCompetenceLevel(decimal competenceLevel) =>
        Set(x => x._competenceLevel = new MonetaryAmount(competenceLevel));

    private OperatorBuilder Set(Action<OperatorBuilder> setter)
    {
        setter(this);
        return this;
    }

    public Operator Build() =>
        new(_login!, _password!, _name!, _competenceLevel!);
}
