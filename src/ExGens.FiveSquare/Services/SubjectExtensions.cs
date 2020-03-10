using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ExGens.FiveSquare.Infrastructure;
using JetBrains.Annotations;

namespace ExGens.FiveSquare.Services
{
  internal static class SubjectExtensions
  {
    public static Result<IEnumerable<T>> CatchAndEmitAll<T>(
      this IObserver<T> subject, [InstantHandle] Func<IEnumerable<T>> source)
      => subject.Catch(source).IfSuccess(_ => EmitAll(subject, _));

    public static void EmitAll<T>(this IObserver<T> subject, [InstantHandle] IEnumerable<T> source, bool complete = true)
    {
      source.Foreach(subject.OnNext);
      if (complete)
      {
        subject.OnCompleted();
      }
    }

    public static Result<TOut> Catch<T,TOut>(this IObserver<T> subject, [InstantHandle] Func<TOut> func)
      => Result.Of(func).IfFail(subject.OnError);

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

    public static IObservable<TOut> SelectAsync<T, TOut>(this IObservable<T> source, Func<T, Task<TOut>> mapper)
      => source.SelectMany(_ => Observable.FromAsync(async () =>  await mapper(_)));
  }
}