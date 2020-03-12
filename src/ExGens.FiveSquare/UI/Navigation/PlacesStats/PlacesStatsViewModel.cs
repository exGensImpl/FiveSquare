using System;
using System.Linq;
using System.Reactive.Linq;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation.PlacesStats
{
  internal sealed class PlacesStatsViewModel : CheckinRangeBasedViewModel
  {
    public PlaceViewModel<Country> Countries { get; set; }

    public PlaceViewModel<string> Cities { get; set; }

    public PlaceViewModel<Venue> Venues { get; set; }
    
    /// <inheritdoc />
    public PlacesStatsViewModel(FiveSquareServices services) : base(services)
    {
      var checkins = GetFilteredCheckins().VenueVisits().ToEnumerable().ToArray();
      Countries = new PlaceViewModel<Country>(checkins.StatsBy(_ => new[]{_.Address.Country}));
      Cities = new PlaceViewModel<string>(checkins.StatsBy(_ => new[]{_.Address.City}));
      Venues = new PlaceViewModel<Venue>(checkins.Stats());
    }
  }
}
