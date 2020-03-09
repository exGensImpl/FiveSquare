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
    }
  }
}