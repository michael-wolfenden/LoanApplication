using System.Text.RegularExpressions;
using JetBrains.Annotations;
using LoanApplication.WebHost.Domain;

public static class Validate
{
    public static PropertiesValidator WithPropertyGroup(string? prefix) =>
        new(prefix);

    public static PropertyInfo<TProperty> Property<TProperty>(TProperty value, [InvokerParameterName] string name) =>
        new(value, name, null);

    public static PropertyInfo<string?> NotWhiteSpace(this PropertyInfo<string?> argument) =>
        string.IsNullOrWhiteSpace(argument.Value)
            ? argument.WithError(Errors.MustNotBeEmpty(argument.Name, argument.PropertyGroup))
            : argument;

    public static PropertyInfo<string?> MaxLength(this PropertyInfo<string?> argument, int maxLength) =>
        argument.Value is not null && argument.Value.Length > maxLength
            ? argument.WithError(Errors.MustNotExceedLength(argument.Name, maxLength, argument.PropertyGroup))
            : argument;

    public static PropertyInfo<string?> Matches(this PropertyInfo<string?> argument, [RegexPattern] string pattern) =>
        !string.IsNullOrWhiteSpace(argument.Value) && !Regex.IsMatch(argument.Value, pattern)
            ? argument.WithError(Errors.MustMatchPattern(argument.Name, pattern, argument.PropertyGroup))
            : argument;

    public static PropertyInfo<TProperty> Positive<TProperty>(this PropertyInfo<TProperty> argument)
        where TProperty : struct, IComparable<TProperty> =>
        Comparer<TProperty>.Default.Compare(argument.Value, default) <= 0
            ? argument.WithError(Errors.MustBePositive(argument.Name, argument.PropertyGroup))
            : argument;

    public static PropertyInfo<string?> Length(this PropertyInfo<string?> argument, int length) =>
        !string.IsNullOrWhiteSpace(argument.Value) && argument.Value.Length != length
            ? argument.WithError(Errors.MustHaveLength(argument.Name, length, argument.PropertyGroup))
            : argument;
}

public class PropertiesValidator
{
    public PropertiesValidator(string? propertyGroup) =>
        PropertyGroup = propertyGroup;

    public string? PropertyGroup { get; }

    public PropertyInfo<TProperty> Property<TProperty>(TProperty value, [InvokerParameterName] string name) =>
        new(value, name, PropertyGroup);
}

public class PropertyInfo<TProperty>
{
    public PropertyInfo(TProperty value, string name, string? propertyGroup)
    {
        Name = name;
        PropertyGroup = propertyGroup;
        Value = value;
    }

    public TProperty Value { get; }

    public string Name { get; }
    public string? PropertyGroup { get; }

    public Error? Error { get; private set; }

    public PropertyInfo<TProperty> WithError(Error? error)
    {
        if (Error == null) Error = error;
        else Error.Combine(error);

        return this;
    }

    public PropertyInfo<TNewProperty> Property<TNewProperty>(TNewProperty value, string name) =>
        new PropertyInfo<TNewProperty>(value, name, PropertyGroup).WithError(Error);
}
