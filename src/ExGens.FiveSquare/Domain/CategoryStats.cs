using System.Collections.Generic;
using System.Linq;

namespace ExGens.FiveSquare.Domain
{
  internal sealed class CategoryStats
  {
    public Category Category { get; }
    public int Places { get; }
    public int Visits { get; }

    private CategoryStats(Category category, int places, int visits)
    {
      Category = category;
      Places = places;
      Visits = visits;
    }

    public static IEnumerable<CategoryStats> FromVisits(IEnumerable<Visit> visits)
    {
      var categories =
        from visit in visits
        from category in visit.Venue.Categories
        group visit by category;

      return categories.Select(_ => new CategoryStats(_.Key, _.Count(), _.Sum(v => v.Times)));
    }
  }
}