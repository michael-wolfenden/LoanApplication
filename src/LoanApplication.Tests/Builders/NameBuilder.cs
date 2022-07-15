using CSharpFunctionalExtensions;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Builders;

public class NameBuilder
{
    private string? _first;
    private string? _last;

    public NameBuilder()
    {
        WithFirst("Peter");
        WithLast("Parker");
    }

    public static NameBuilder GivenName() =>
        new();

    public NameBuilder WithFirst(string first) =>
        Set(x => x._first = first);

    public NameBuilder WithLast(string last) =>
        Set(x => x._last = last);

    private NameBuilder Set(Action<NameBuilder> setter)
    {
        setter(this);
        return this;
    }

    public Result<Name, Error> Build() =>
        Name.Of(_first, _last);
}
