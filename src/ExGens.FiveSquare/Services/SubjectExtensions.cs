using System;
using System.Collections.Generic;

namespace ExGens.FiveSquare.Services
{
  internal static class SubjectExtensions
  {
    public static bool CatchAndEmitAll<T>(this IObserver<T> subject, Func<IEnumerable<T>> source)
    {
      var (result, success) = Catch(subject, source);
      if (success)
      {
        EmitAll(subject, result);
      }

      return success;
    }

    public static void EmitAll<T>(this IObserver<T> subject, IEnumerable<T> source)
    {
      foreach (var item in source)
      {
        subject.OnNext(item);
      }
      subject.OnCompleted();
    }

    public static (TOut,bool) Catch<T,TOut>(this IObserver<T> subject, Func<TOut> func)
    {
      try
      {
        return (func(),true);
      }
      catch (Exception ex)
      {
        subject.OnError(ex);
        return (default,false);
      }
    }

  }
}