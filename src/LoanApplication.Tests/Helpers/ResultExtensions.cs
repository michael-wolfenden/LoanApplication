using CSharpFunctionalExtensions;
using FluentAssertions;
using LoanApplication.WebHost.Domain;

namespace LoanApplication.Tests.Helpers;

public static class ResultExtensions
{
    public static void ShouldFailWithMessage<T>(this Result<T, Error> subject, string message)
    {
        subject.IsFailure.Should().BeTrue();
        subject.Error.Reasons.Single().Message.Should().Be(message);
    }

    public static void ShouldSucceed<T>(this Result<T, Error> subject) =>
        subject.IsSuccess.Should().BeTrue();
}
