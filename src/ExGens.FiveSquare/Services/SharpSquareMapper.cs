using System.Linq;
using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Entities;
using Category = ExGens.FiveSquare.Domain.Category;

namespace ExGens.FiveSquare.Services
{
  internal static class SharpSquareMapper
  {
    public static Coordinates ToCoordinates(this Location location)
      => new Coordinates(location.lat, location.lng);

    public static Visit ToVisit(this VenueHistory history)
      => new Visit(
        history.venue.ToVenue(),  
        int.Parse(history.beenHere));

    public static Category ToCategory(this FourSquare.SharpSquare.Entities.Category category)
      => Category.GetOrCreate(category.id, category.shortName);

    public static Place ToVenue(this Venue venue)
      => new Place(
        venue.name, 
        venue.location.ToCoordinates(), 
        venue.categories.Select(ToCategory).ToArray());

  }
}