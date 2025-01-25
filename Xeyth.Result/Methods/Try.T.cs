﻿using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Attempts to execute the specified <paramref name="action"/>. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <typeparam name="TValue">The type of the value returned by <paramref name="action"/>.</typeparam>
    /// <param name="action">The function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.ExceptionalFactory"/>.</param>
    /// <returns>A successful <see cref="Result{TValue}"/> containing the result of <paramref name="action"/>, or a failed result with an error generated by <paramref name="exceptionHandler"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    public static Result<TValue> Try<TValue>(Func<TValue> action, Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            return Ok(action());
        }
        catch (Exception exception)
        {
            exceptionHandler ??= exception => new ExceptionalError(exception);
            return Fail(exceptionHandler(exception));
        }
    }

    /// <summary>Attempts to execute the specified <paramref name="action"/> that returns a <see cref="Result{TValue}"/>. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <typeparam name="TValue">The type of the value contained in the <see cref="Result{TValue}"/> returned by <paramref name="action"/>.</typeparam>
    /// <param name="action">The function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.ExceptionalFactory"/>.</param>
    /// <returns>The <see cref="Result{TValue}"/> returned by <paramref name="action"/>, or a failed result with an error generated by <paramref name="exceptionHandler"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    public static Result<TValue> Try<TValue>(Func<Result<TValue>> action, Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            return action();
        }
        catch (Exception exception)
        {
            exceptionHandler ??= exception => new ExceptionalError(exception);
            return Fail(exceptionHandler(exception));
        }
    }
}