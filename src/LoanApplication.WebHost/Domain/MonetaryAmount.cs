using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class MonetaryAmount : SimpleValueObject<decimal>, IComparable<MonetaryAmount>
{
    public MonetaryAmount(decimal value) : base(decimal.Round(value, 2, MidpointRounding.ToEven))
    {
    }

    public int CompareTo(MonetaryAmount? other) =>
        Value.CompareTo(other);

    public MonetaryAmount Add(MonetaryAmount other) =>
        new(Value + other.Value);

    public MonetaryAmount Subtract(MonetaryAmount other) =>
        new(Value - other.Value);

    public MonetaryAmount MultiplyByPercent(Percent percent) =>
        new(Value * percent.Value / 100M);

    public override int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is MonetaryAmount other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(MonetaryAmount)}");
    }

    public static bool operator <(MonetaryAmount? left, MonetaryAmount? right) =>
        Comparer<MonetaryAmount>.Default.Compare(left, right) < 0;

    public static bool operator >(MonetaryAmount? left, MonetaryAmount? right) =>
        Comparer<MonetaryAmount>.Default.Compare(left, right) > 0;

    public static bool operator <=(MonetaryAmount? left, MonetaryAmount? right) =>
        Comparer<MonetaryAmount>.Default.Compare(left, right) <= 0;

    public static bool operator >=(MonetaryAmount? left, MonetaryAmount? right) =>
        Comparer<MonetaryAmount>.Default.Compare(left, right) >= 0;

    public static MonetaryAmount operator +(MonetaryAmount one, MonetaryAmount two) =>
        one.Add(two);

    public static MonetaryAmount operator -(MonetaryAmount one, MonetaryAmount two) =>
        one.Subtract(two);

    public static MonetaryAmount operator *(MonetaryAmount one, Percent percent) =>
        one.MultiplyByPercent(percent);
}
