using System;
using System.Collections.Generic;
using System.Windows;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.UI.Wpf;
using Point = Mapsui.Geometries.Point;

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

    public Point Location
    {
      get => (Point)GetValue(LocationProperty);
      set => SetValue(LocationProperty, value);
    }

    public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
      name: nameof(Location),
      propertyType: typeof(Point),
      ownerType: typeof(BindableMap),
      typeMetadata: new PropertyMetadata(null, LocationPropertyChanged) );

    private static void LocationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is BindableMap map && e.NewValue is Point location)
      {
        map.ChangeLocation(location);
      }
    }

    private void ChangeLocation(Point location)
    {
      var coordinate = SphericalMercator.FromLonLat(location.X, location.Y);
      //Map.Home = n => n.NavigateTo(coordinate, Map.Resolutions[9]);
      Navigator.CenterOn(coordinate);
      //Navigator.NavigateTo(coordinate, Map.Resolutions[9]);
    }

    #endregion
  }
}