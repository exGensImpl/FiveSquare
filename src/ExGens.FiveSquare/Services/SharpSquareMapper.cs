using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Entities;
using Category = ExGens.FiveSquare.Domain.Category;
using Checkin = ExGens.FiveSquare.Domain.Checkin;
using Venue = ExGens.FiveSquare.Domain.Venue;

namespace ExGens.FiveSquare.Services
{
  internal static class SharpSquareMapper
  {
    private static readonly CityDictionary m_cityDictionary = CityDictionaryFactory.Load("../../../../../data/cities.txt");

    public static Coordinates ToCoordinates(this Location location)
      => new Coordinates(location.lat, location.lng);

    public static Visits<Venue> ToVisit(this VenueHistory history)
      => new Visits<Venue>(
        history.venue.ToPlace(),  
        int.Parse(history.beenHere));

    public static Category ToCategory(this FourSquare.SharpSquare.Entities.Category category)
      => Category.GetOrCreate(category.id, category.shortName);

    public static Venue ToPlace(this FourSquare.SharpSquare.Entities.Venue venue)
      => new Venue(
        venue.id,
        venue.name, 
        venue.location.ToAddress(), 
        venue.categories.Select(ToCategory).ToArray());

    public static Address ToAddress(this Location location)
      => new Address(
        location.address,
        new Country(location.country), 
        m_cityDictionary.Map(location.city ?? ""),
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

  internal static class CityDictionaryFactory
  {
    public static CityDictionary Load(string Filepath)
    {
      try
      {
        return new CityDictionary(
          File.ReadLines(Filepath)
            .Select(_ => _.Split('|'))
            .ToDictionary(_ => _[0], _ => _[1])
        );
      }
      catch
      {
        return new CityDictionary(new Dictionary<string, string>());
      }
    }
  }

  internal sealed class CityDictionary
  {
    private readonly IReadOnlyDictionary<string, string> m_cities;

    public CityDictionary(IReadOnlyDictionary<string, string> Names)
    {
      m_cities = Names;
    }

    public string Map(string originName)
    {
      return originName != null && m_cities.ContainsKey(originName) ? 
        m_cities[originName] : 
        originName;
    }
  }
}