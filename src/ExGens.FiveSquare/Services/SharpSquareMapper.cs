using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Entities;

namespace ExGens.FiveSquare.Services
{
  internal static class SharpSquareMapper
  {
    public static Coordinates ToCoordinates(this Location location)
      => new Coordinates(location.lat, location.lng);

    public static Visit ToVisit(this VenueHistory history)
      => new Visit(
        ToCoordinates(history.venue.location),  
        int.Parse(history.beenHere));
  }
}