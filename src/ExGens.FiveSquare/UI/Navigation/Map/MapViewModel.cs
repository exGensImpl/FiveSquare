using System.Collections.Generic;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Services;
using Mapsui.Layers;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class MapViewModel : ViewModelBase
  {
    public Person User { get; }

    public IEnumerable<ILayer> Layers
    {
      get => m_layers;
      set => OnPropertyChanged(ref m_layers, value );
    }

    public Coordinates Location
    {
      get => m_location;
      set => OnPropertyChanged(ref m_location, value);
    }

    private IEnumerable<ILayer> m_layers;
    private Coordinates m_location;

    public MapViewModel(FiveSquareServices services)
    {
      var layerFactory = new LayerFactory(LayerSettings.Default);

      User = services.FiveSquare.User;
      Location = User.Home;
      Layers = new ILayer[]
      {
        layerFactory.Map(),
        layerFactory.Checkins(services.FiveSquare.GetVisits())
      };
    }
  }
}