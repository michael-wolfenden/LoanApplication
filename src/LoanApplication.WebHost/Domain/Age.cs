using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Age : SimpleValueObject<int>
{
    public static readonly Age SixtyFive = 65.Years();

    public Age(int value) : base(value)
    {
    }

    public static Age Between(DateOnly start, DateOnly end) =>
        new(end.Year - start.Year);
}

public static class AgeExtensions
{
    public static Age Years(this int age) =>
        new(age);
}
