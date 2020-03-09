using System;
using System.Collections.Generic;

namespace ExGens.FiveSquare.Infrastructure
{
  public static class Extensions
  {
    public static void Foreach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (var item in source)
      {
        action(item);
      }
    }

    public static IEnumerable<T> Unfold<T>(this T start, Func<T, T> next)
    {
      var elem = start;
      while (true)
      {
        yield return elem;
        elem = next(elem);
      }
      // ReSharper disable once IteratorNeverReturns
      // It's a sequence generator
    }

    public static TValue GetOrElse<TKey, TValue>(
      this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue fallback)
      => dictionary.ContainsKey(key) ? dictionary[key] : fallback;

    public static T To<T>(this T value, Action<T> action)
    {
      action(value);
      return value;
    }

    public static TOut To<T, TOut>(this T value, Func<T, TOut> func) => func(value);
  }
}