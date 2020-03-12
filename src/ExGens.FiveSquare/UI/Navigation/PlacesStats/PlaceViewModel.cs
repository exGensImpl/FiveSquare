using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.PlacesStats
{
  internal sealed class PlaceViewModel<T>
  {
    public IReadOnlyList<Stats<T>> Stats { get; set; }

    public int MaxVisits => Stats.Max(_ => _.Visits);

    public int MaxPlaces => Stats.Max(_ => _.Places);
    
    public PlaceViewModel(IEnumerable<Stats<T>> stats)
    {
      Stats = stats.OrderByDescending(_ => _.Visits).ToArray();
    }
  }
}