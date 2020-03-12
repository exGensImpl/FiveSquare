using System;
using System.Collections.Generic;
using System.Linq;

namespace ExGens.FiveSquare.Domain
{
  internal sealed class Stats<T>
  {
    public T Location { get; }
    public int Places { get; }
    public int Visits { get; }

    public Stats(T location, int places, int visits)
    {
      Location = location;
      Places = places;
      Visits = visits;
    }
  }

  internal static class StatsExtensions
  {
    public static IEnumerable<Stats<T>> Stats<T>(this IEnumerable<Visits<T>> visits)
      => visits.StatsBy(_ => new[] {_});

    public static IEnumerable<Stats<T>> StatsBy<T, TVisit>(this IEnumerable<Visits<TVisit>> visits, Func<TVisit, IEnumerable<T>> statSelector)
    {
      var stats =
        from visit in visits
        from stat in statSelector(visit.Place)
        group visit by stat;

      return stats.Select(_ => new Stats<T>(_.Key, _.Count(), _.Sum(v => v.Times)));
    }
  }
}