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
  }
}