using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ExGens.FiveSquare.Infrastructure;

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

    public static void EmitAll<T>(this IObserver<T> subject, IEnumerable<T> source, bool complete = true)
    {
      source.Foreach(subject.OnNext);
      if (complete)
      {
        subject.OnCompleted();
      }
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

    public static IObservable<T> ColdObservable<T>(Func<IEnumerable<T>> func)
    {
      return Observable.Create<T>(o =>
      {
        Task.Run(() => o.CatchAndEmitAll(func));
        return Disposable.Empty;
      });
    }

    public static IObservable<T> ColdObservable<T>(Action<IObserver<T>> action)
    {
      return Observable.Create<T>(o =>
      {
        Task.Run(() => action(o));
        return Disposable.Empty;
      });
    }
  }
}