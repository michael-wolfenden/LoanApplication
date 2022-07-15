using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Registration : ValueObject
{
    public Registration(DateOnly registrationDate, Operator registeredBy)
    {
        RegistrationDate = registrationDate;
        RegisteredBy = registeredBy.Id;
    }

    //To satisfy EF Core, disable non nullable checks
#pragma warning disable CS8618
    protected Registration()
#pragma warning restore CS8618
    {
    }

    public DateOnly RegistrationDate { get; }

    public OperatorId RegisteredBy { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RegistrationDate;
        yield return RegisteredBy;
    }
}
