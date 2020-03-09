using System;
using System.Runtime.ExceptionServices;

namespace ExGens.FiveSquare.Infrastructure
{
  internal static class Result
  {
    public static Result<T> Of<T>(Func<T> action)
    {
      try
      {
        return Result<T>.Success(action());
      }
      catch (Exception error)
      {
        return Result<T>.Fail(error);
      }
    }
  }

  internal readonly struct Result<T>
  {
    private readonly T m_value;

    private readonly ExceptionDispatchInfo m_errorInfo;

    public bool IsSuccess => m_errorInfo == null;

    public bool IsFail => m_errorInfo != null;

    public static Result<T> Success(T value) => new Result<T>(value, default);

    public static Result<T> Fail(ExceptionDispatchInfo errorInfo) => new Result<T>(default, errorInfo);

    public static Result<T> Fail(Exception error) => new Result<T>(default, ExceptionDispatchInfo.Capture(error));

    private Result(T value, ExceptionDispatchInfo errorInfo)
    {
      m_value = value;
      m_errorInfo = errorInfo;
    }

    public Result<TOut> Map<TOut>(Func<T, TOut> mapper)
      => IsSuccess ? Result<TOut>.Success(mapper(m_value)) : Result<TOut>.Fail(m_errorInfo);

    public T Recover(T fallback) => IsSuccess ? m_value : fallback;

    public Result<T> IfSuccess(Action<T> action)
    {
      if (IsSuccess)
      {
        action(m_value);
      }

      return this;
    }

    public Result<T> IfFail(Action<Exception> action)
    {
      if (IsFail)
      {
        action(m_errorInfo.SourceException);
      }

      return this;
    }

    public void Throw()
    {
      if (IsFail)
      {
        m_errorInfo.Throw();
      }
    }
  }
}