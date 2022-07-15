namespace LoanApplication.WebHost.Domain;

public interface IOperators
{
    Task<Operator?> WithLogin(Login login);

    Task Add(Operator @operator);
}
