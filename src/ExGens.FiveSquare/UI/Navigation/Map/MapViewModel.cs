using System.Collections.Generic;
using ExGens.FiveSquare.Services;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Utilities;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class MapViewModel : ViewModelBase
  {
    public IEnumerable<ILayer> Layers
    {
      get => m_layers;
      set => OnPropertyChanged(ref m_layers, value );
    }

    public Point Location
    {
      get => _location;
      set => OnPropertyChanged(ref _location, value);
    }

    private readonly FiveSquareServices m_services;
    private IEnumerable<ILayer> m_layers;
    private Point _location;

    public MapViewModel(FiveSquareServices services)
    {
      m_services = services;

      Layers = new [] { OpenStreetMap.CreateTileLayer() };
      Location = new Point(30.3, 59.94);
    }
  }
}