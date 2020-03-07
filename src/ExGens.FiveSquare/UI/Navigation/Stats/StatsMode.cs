using System;
using ExGens.FiveSquare.Properties;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal sealed class StatsMode : IModeFactory
  {
    /// <inheritdoc />
    public string Title => Resources.StatsView_Title;

    /// <inheritdoc />
    public Type ViewType => typeof(StatsView);

    /// <inheritdoc />
    public FiveSquareServices Services { get; set; }

    /// <inheritdoc />
    public IViewModel CreateViewModel()
    {
      return new StatsViewModel(Services);
    }
  }
}
