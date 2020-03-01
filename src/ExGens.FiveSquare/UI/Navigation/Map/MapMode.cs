using System;
using ExGens.FiveSquare.Properties;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class MapMode : IModeFactory
  {
    /// <inheritdoc />
    public Type ViewType => typeof(MapView);

    /// <inheritdoc />
    public FiveSquareServices Services { get; set; }

    /// <inheritdoc />
    public string Title => Resources.Map_Title;
  
    /// <inheritdoc />
    public ViewModelBase CreateViewModel()
    {
      return new MapViewModel( Services );
    }
  }
}