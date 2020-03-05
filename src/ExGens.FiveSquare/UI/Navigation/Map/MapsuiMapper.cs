using System.Collections.Generic;
using ExGens.FiveSquare.Domain;
using Mapsui.Geometries;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal static class MapsuiMapper
  {
    public static Point ToMercator(this Coordinates coordinates)
      => SphericalMercator.FromLonLat(
        coordinates.Longitude,
        coordinates.Latitude);
    
    public static IFeature ToFeature(this Visit visit, ICollection<IStyle> styles)
      => new Feature
      {
        Styles = styles,
        Geometry = visit.Venue.Location.ToMercator(),
        ["name"] = visit.Venue.Name
      };
  }
}