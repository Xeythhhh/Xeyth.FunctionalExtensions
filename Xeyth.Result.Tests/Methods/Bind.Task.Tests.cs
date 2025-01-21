﻿using Shouldly;

namespace Xeyth.Result.Tests.Methods;

public class BindAsync : TestBase
{
    [Fact]
    public async Task ShouldBind_WhenInitialResultIsSuccessful_AndBindingResultIsSuccessful() =>
        await Verify(
            async () => await Result.Ok().WithSuccess("Initial Result Success")
                .Bind(() => Task.FromResult(Result.Ok().WithSuccess("Binding Result Success"))),
            Settings);

    [Fact]
    public async Task ShouldBind_WhenInitialResultIsSuccessful_AndBindingResultIsFailure() =>
        await Verify(
            async () => await Result.Ok().WithSuccess("Initial Result Success")
                .Bind(() => Task.FromResult(Result.Fail("Binding Result Error"))),
            Settings);

    [Fact]
    public async Task ShouldNotBind_WhenInitialResultIsFailure_AndBindingResultIsSuccessful() =>
        await Verify(
            async () => await Result.Fail("Initial Result Error")
                .Bind(() => Task.FromResult(Result.Ok().WithSuccess("Binding Result Success"))),
            Settings);

    [Fact]
    public async Task ShouldNotBind_WhenInitialResultIsFailure_AndBindingResultIsFailure() =>
        await Verify(
            async () => await Result.Fail("Initial Result Error")
                .Bind(() => Task.FromResult(Result.Fail("Binding Result Error"))),
            Settings);

    [Fact]
    public async Task ShouldBind_WhenInitialResultIsSuccessful_AndGenericBindingResultIsSuccessful() =>
        await Verify(
            async () => await Result.Ok().WithSuccess("Initial Result Success")
                .Bind(() => Task.FromResult(Result.Ok(420).WithSuccess("Binding Result Success"))),
            Settings);

    [Fact]
    public async Task ShouldBind_WhenInitialResultIsSuccessful_AndGenericBindingResultIsFailure() =>
        await Verify(
            async () => await Result.Ok().WithSuccess("Initial Result Success")
                .Bind(() => Task.FromResult(Result.Fail<int>("Binding Result Error"))),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public async Task ShouldNotBind_WhenInitialResultIsFailure_AndGenericBindingResultIsSuccessful() =>
        await Verify(
            async () => await Result.Fail("Initial Result Error")
                .Bind(() => Task.FromResult(Result.Ok(420).WithSuccess("Binding Result Success"))),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public async Task ShouldNotBind_WhenInitialResultIsFailure_AndGenericBindingResultIsFailure() =>
        await Verify(
            async () => await Result.Fail("Initial Result Error")
                .Bind(() => Task.FromResult(Result.Fail<int>("Binding Result Error"))),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenBindActionIsNull() =>
        await Should.ThrowAsync<ArgumentNullException>(() => Result.Ok().Bind((Func<Task<Result>>)null!));

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenGenericBindActionIsNull() =>
        await Should.ThrowAsync<ArgumentNullException>(() => Result.Ok().Bind((Func<Task<Result<object>>>)null!));
}
