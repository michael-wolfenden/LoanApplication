using CSharpFunctionalExtensions;

namespace LoanApplication.WebHost.Domain;

public class Error : ICombine
{
    private readonly List<Reason> _reasons = new();

    public Error(Reason reason) =>
        _reasons.Add(reason);

    public IReadOnlyList<Reason> Reasons => _reasons.AsReadOnly();

    public ICombine Combine(ICombine? other)
    {
        if (other is null) return this;

        if (other is not Error otherAsError)
            throw new Exception("Can only combine with another object of type Error");

        _reasons.AddRange(otherAsError.Reasons);
        return this;
    }

    public static Error Failure(string property, string message) =>
        new(new Reason(ReasonType.Failure, property, message));

    public static Error Unexpected(string property, string message) =>
        new(new Reason(ReasonType.Unexpected, property, message));

    public static Error Validation(string property, string message) =>
        new(new Reason(ReasonType.Validation, property, message));

    public static Error Validation(string property, string message, string? propertyGroup) =>
        new(new Reason(ReasonType.Validation, property, message, propertyGroup));

    public static Error Conflict(string property, string message) =>
        new(new Reason(ReasonType.Conflict, property, message));

    public static Error NotFound(string property, string message) =>
        new(new Reason(ReasonType.NotFound, property, message));
}

public record Reason(ReasonType Type, string Property, string Message, string? PropertyGroup = null);

public enum ReasonType
{
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound
}

public static class ErrorExtensions
{
    public static Result<(T1, T2), Error> Combine<T1, T2>(this (Result<T1, Error>, Result<T2, Error>) results) =>
        Result.Combine<Error>(results.Item1, results.Item2).Map(() => (results.Item1.Value, results.Item2.Value));

    public static Result<(T1, T2, T3), Error> Combine<T1, T2, T3>(this (Result<T1, Error>, Result<T2, Error>, Result<T3, Error>) results) =>
        Result.Combine<Error>(results.Item1, results.Item2, results.Item3).Map(() => (results.Item1.Value, results.Item2.Value, results.Item3.Value));

    public static Result<(T1, T2, T3, T4), Error> Combine<T1, T2, T3, T4>(
        this (Result<T1, Error>, Result<T2, Error>, Result<T3, Error>, Result<T4, Error>) results) =>
        Result.Combine<Error>(results.Item1, results.Item2, results.Item3, results.Item4)
            .Map(() => (results.Item1.Value, results.Item2.Value, results.Item3.Value, results.Item4.Value));

    public static Result<(T1, T2, T3, T4, T5), Error> Combine<T1, T2, T3, T4, T5>(
        this (Result<T1, Error>, Result<T2, Error>, Result<T3, Error>, Result<T4, Error>, Result<T5, Error>) results) =>
        Result.Combine<Error>(results.Item1, results.Item2, results.Item3, results.Item4, results.Item5).Map(() =>
            (results.Item1.Value, results.Item2.Value, results.Item3.Value, results.Item4.Value, results.Item5.Value));

    public static Result<(T1, T2, T3, T4, T5, T6), Error> Combine<T1, T2, T3, T4, T5, T6>(
        this (Result<T1, Error>, Result<T2, Error>, Result<T3, Error>, Result<T4, Error>, Result<T5, Error>, Result<T6, Error>) results) =>
        Result.Combine<Error>(results.Item1, results.Item2, results.Item3, results.Item4, results.Item5, results.Item6).Map(() =>
            (results.Item1.Value, results.Item2.Value, results.Item3.Value, results.Item4.Value, results.Item5.Value, results.Item6.Value));

    public static Result<(T1, T2, T3, T4, T5, T6, T7), Error> Combine<T1, T2, T3, T4, T5, T6, T7>(
        this (Result<T1, Error>, Result<T2, Error>, Result<T3, Error>, Result<T4, Error>, Result<T5, Error>, Result<T6, Error>, Result<T7, Error>)
            results) =>
        Result.Combine<Error>(results.Item1, results.Item2, results.Item3, results.Item4, results.Item5, results.Item6, results.Item7).Map(() =>
            (results.Item1.Value, results.Item2.Value, results.Item3.Value, results.Item4.Value, results.Item5.Value, results.Item6.Value,
                results.Item7.Value));

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8), Error> Combine<T1, T2, T3, T4, T5, T6, T7, T8>(
        this (Result<T1, Error>, Result<T2, Error>, Result<T3, Error>, Result<T4, Error>, Result<T5, Error>, Result<T6, Error>, Result<T7, Error>,
            Result<T8, Error>) results) =>
        Result.Combine<Error>(results.Item1, results.Item2, results.Item3, results.Item4, results.Item5, results.Item6, results.Item7, results.Item8)
            .Map(() => (results.Item1.Value, results.Item2.Value, results.Item3.Value, results.Item4.Value, results.Item5.Value, results.Item6.Value,
                results.Item7.Value, results.Item8.Value));

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9), Error> Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        this (Result<T1, Error>, Result<T2, Error>, Result<T3, Error>, Result<T4, Error>, Result<T5, Error>, Result<T6, Error>, Result<T7, Error>,
            Result<T8, Error>, Result<T9, Error>) results) =>
        Result
            .Combine<Error>(results.Item1, results.Item2, results.Item3, results.Item4, results.Item5, results.Item6, results.Item7, results.Item8,
                results.Item9).Map(() => (results.Item1.Value, results.Item2.Value, results.Item3.Value, results.Item4.Value, results.Item5.Value,
                results.Item6.Value, results.Item7.Value, results.Item8.Value, results.Item9.Value));
}
