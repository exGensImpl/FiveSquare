using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExGens.FiveSquare.Properties;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation.PlacesStats
{
  class PlacesStatsMode : IModeFactory
  {
    /// <inheritdoc />
    public string Title => Resources.PlacesStatsView_Title;

    /// <inheritdoc />
    public Type ViewType => typeof(PlacesStatsView);

    /// <inheritdoc />
    public FiveSquareServices Services { get; set; }

    /// <inheritdoc />
    public IViewModel CreateViewModel()
    {
      return new PlacesStatsViewModel(Services);
    }
  }
}
