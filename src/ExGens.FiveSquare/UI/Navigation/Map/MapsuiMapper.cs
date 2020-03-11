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
    
    public static IFeature ToFeature(this Visits<Venue> visit, ICollection<IStyle> styles)
      => new Feature
      {
        Styles = styles,
        Geometry = visit.Place.Address.Location.ToMercator(),
        ["name"] = visit.Place.Name
      };
  }
}