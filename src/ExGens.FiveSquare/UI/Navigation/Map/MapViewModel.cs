﻿using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Services;
using Mapsui.Layers;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class MapViewModel : ViewModelBase
  {
    public IEnumerable<ILayer> Layers
    {
      get => m_layers;
      set => OnPropertyChanged(ref m_layers, value );
    }

    public Coordinates Location
    {
      get => _location;
      set => OnPropertyChanged(ref _location, value);
    }

    private readonly FiveSquareServices m_services;
    private IEnumerable<ILayer> m_layers;
    private Coordinates _location;

    public MapViewModel(FiveSquareServices services)
    {
      m_services = services;

      Location = services.FiveSquare.User.Home;
      Layers = new ILayer[]
      {
        LayerFactory.Map(),
        LayerFactory.Checkins(m_services.FiveSquare.GetVisits().ToArray())
      };
    }
  }
}