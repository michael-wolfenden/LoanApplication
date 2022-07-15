namespace LoanApplication.WebHost.Domain;

public class Errors
{
    public static Error NotFound(string property, string identifier) =>
        Error.NotFound(property, $"{property} could not be found with identifier {identifier}");

    public static Error MustNotBeEmpty(string property, string? propertyGroup = null) =>
        Error.Validation(property, $"{property} cannot be empty or consist only of white-space characters.", propertyGroup);

    public static Error MustNotExceedLength(string property, int maxLength, string? propertyGroup = null) =>
        Error.Validation(property, $"{property} cannot be longer than {maxLength} characters.", propertyGroup);

    public static Error MustMatchPattern(string property, string pattern, string? propertyGroup = null) =>
        Error.Validation(property, $"{property} did not match pattern {pattern}.", propertyGroup);

    public static Error MustBePositive(string property, string? propertyGroup = null) =>
        Error.Validation(property, $"{property} must be greater than zero.", propertyGroup);

    public static Error MustHaveLength(string property, int length, string? propertyGroup = null) =>
        Error.Validation(property, $"{property} must consist of {length} characters.", propertyGroup);

    public static Error CannotAcceptApplicationThatIsAcceptedOrRejected() =>
        Error.Failure("Loan application", "Cannot accept application that is already accepted or rejected");

    public static Error CannotRejectApplicationThatIsAcceptedOrRejected() =>
        Error.Failure("Loan application", "Cannot reject application that is already accepted or rejected");

    public static Error CannotAcceptApplicationBeforeScoring() =>
        Error.Failure("Loan application", "Cannot accept application before scoring");

    public static Error OperatorDoesNotHaveCompetenceLevel() =>
        Error.Failure("Loan application", "Operator does not have required competence level to accept application");
}
