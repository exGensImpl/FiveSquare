using ExGens.FiveSquare.Domain;
using Mapsui.Geometries;
using Mapsui.Projection;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal static class MapsuiMapper
  {
    public static Point ToMercator(this Coordinates coordinates)
      => SphericalMercator.FromLonLat(
        coordinates.Longitude,
        coordinates.Latitude);
  }
}