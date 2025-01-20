﻿using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Executes the specified <paramref name="action"/> and returns a <see cref="Result"/> indicating success or failure. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.DefaultExceptionalErrorFactory"/>.</param>
    /// <returns>A successful <see cref="Result"/> if the <paramref name="action"/> executes without exceptions; otherwise, a failed <see cref="Result"/> with an error generated by <paramref name="exceptionHandler"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    public static Result Try(Action action, Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            action();
            return Ok();
        }
        catch (Exception exception)
        {
            exceptionHandler ??= Error.DefaultExceptionalErrorFactory;
            return Fail(exceptionHandler(exception));
        }
    }

    /// <summary>Executes the specified <paramref name="action"/> and returns a <see cref="Result"/> indicating success or failure. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.DefaultExceptionalErrorFactory"/>.</param>
    /// <returns>A successful <see cref="Result"/> if the <paramref name="action"/> executes without exceptions; otherwise, a failed <see cref="Result"/> with an error generated by <paramref name="exceptionHandler"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    public static Result Try(Func<Result> action, Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            return action();
        }
        catch (Exception exception)
        {
            exceptionHandler ??= Error.DefaultExceptionalErrorFactory;
            return Fail(exceptionHandler(exception));
        }
    }
}
