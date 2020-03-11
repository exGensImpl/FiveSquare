using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Maps chekin lists to visits of different places
  /// </summary>
  internal static class VisitMapper
  {
    public static IEnumerable<Visits<Venue>> VenueVisits(this IEnumerable<Checkin> checkins)
      => checkins.GroupBy(_ => _.Location).Select(_ => new Visits<Venue>(_.Key, _.Count()));

    public static IObservable<Visits<Venue>> VenueVisits(this IObservable<Checkin> checkins)
      => checkins.GroupBy(_ => _.Location)
        .SelectAsync(async _ => new Visits<Venue>(_.Key, await Observable.Count<Checkin>(_)));

    public static IEnumerable<Visits<string>> CityVisits(this IEnumerable<Checkin> checkins)
      => checkins.GroupBy(_ => _.Location.Address.City)
        .Select(_ => new Visits<string>(_.Key, _.Count()));

    public static IObservable<Visits<string>> CityVisits(this IObservable<Checkin> checkins)
      => checkins.GroupBy(_ => _.Location.Address.City)
        .SelectAsync(async _ => new Visits<string>(_.Key, await _.Count()));

    public static IEnumerable<Visits<Country>> CountryVisits(this IEnumerable<Checkin> checkins)
      => checkins.GroupBy(_ => _.Location.Address.Country)
        .Select(_ => new Visits<Country>(_.Key, _.Count()));

    public static IObservable<Visits<Country>> CountryVisits(this IObservable<Checkin> checkins)
      => checkins.GroupBy(_ => _.Location.Address.Country)
        .SelectAsync(async _ => new Visits<Country>(_.Key, await _.Count()));
  }
}