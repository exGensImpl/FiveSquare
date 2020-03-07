using System;
using System.Linq;
using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Entities;
using Category = ExGens.FiveSquare.Domain.Category;
using Checkin = ExGens.FiveSquare.Domain.Checkin;

namespace ExGens.FiveSquare.Services
{
  internal static class SharpSquareMapper
  {
    public static Coordinates ToCoordinates(this Location location)
      => new Coordinates(location.lat, location.lng);

    public static Visit ToVisit(this VenueHistory history)
      => new Visit(
        history.venue.ToPlace(),  
        int.Parse(history.beenHere));

    public static Category ToCategory(this FourSquare.SharpSquare.Entities.Category category)
      => Category.GetOrCreate(category.id, category.shortName);

    public static Place ToPlace(this Venue venue)
      => new Place(
        venue.id,
        venue.name, 
        venue.location.ToAddress(), 
        venue.categories.Select(ToCategory).ToArray());

    public static Address ToAddress(this Location location)
      => new Address(
        location.address,
        location.country, 
        location.ToCoordinates());

    public static Checkin ToCheckin(this FourSquare.SharpSquare.Entities.Checkin checkin)
      => new Checkin(
        long.Parse(checkin.createdAt).ToDateTime(),
        checkin.venue.ToPlace());

    public static DateTime ToDateTime( this long unixTimeStamp )
      => new DateTime(1970,1,1,0,0,0,0, DateTimeKind.Utc)
        .AddSeconds( unixTimeStamp )
        .ToLocalTime();
  }
}