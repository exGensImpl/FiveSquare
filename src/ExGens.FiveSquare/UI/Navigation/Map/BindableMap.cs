using System;
using System.Collections.Generic;
using System.Windows;
using ExGens.FiveSquare.Domain;
using Mapsui.Layers;
using Mapsui.UI.Wpf;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal class BindableMap : MapControl
  {
    #region LayerSource
    
    public IEnumerable<ILayer> LayerSource
    {
      get => (IEnumerable<ILayer>)GetValue(LayerSourceProperty);
      set => SetValue(LayerSourceProperty, value);
    }

    public static readonly DependencyProperty LayerSourceProperty = DependencyProperty.Register(
      name: nameof(LayerSource),
      propertyType: typeof(IEnumerable<ILayer>),
      ownerType: typeof(BindableMap),
      typeMetadata: new PropertyMetadata(Array.Empty<ILayer>(), LayerSourcePropertyChanged) );

    private static void LayerSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is BindableMap map && e.NewValue is IEnumerable<ILayer> layers)
      {
        map.ChangeLayers(layers);
      }
    }

    private void ChangeLayers(IEnumerable<ILayer> layers)
    {
      Map.Layers.Clear();
      foreach (var layer in layers)
      {
        Map.Layers.Add(layer);
      }
    }

    #endregion

    #region Location

    public Coordinates Location
    {
      get => (Coordinates)GetValue(LocationProperty);
      set => SetValue(LocationProperty, value);
    }

    public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
      name: nameof(Location),
      propertyType: typeof(Coordinates),
      ownerType: typeof(BindableMap),
      typeMetadata: new PropertyMetadata(default(Coordinates), LocationPropertyChanged) );

    private static void LocationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is BindableMap map && e.NewValue is Coordinates location)
      {
        map.Navigator.CenterOn(location.ToMercator());
      }
    }

    #endregion
  }
}