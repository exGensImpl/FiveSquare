using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquare
  {
    private readonly SharpSquare m_client;

    public Person User => GetCurrentUserInfo();
    
    public IReadOnlyList<Visit> GetVisits()
      => m_client.GetUserVenueHistory().Select(_ => _.ToVisit()).ToArray();

    public IReadOnlyList<Checkin> GetCheckins() 
      => m_client.GetUserCheckins(
                    "self",
                    new Dictionary<string, string>{ ["limit"] = "250" })
                 .Select(_ => _.ToCheckin())
                 .ToArray();

    public FiveSquare(SharpSquare client)
    {
      m_client = client;
    }

    private Person GetCurrentUserInfo()
    {
      try
      {
        var user = m_client.GetUser("self");
        var lastCheckinLocation = user.checkins.items.FirstOrDefault()?.venue?.location;
        return new Person(
          user.firstName,
          lastCheckinLocation?.ToCoordinates() ?? default);
      }
      catch
      {
        return new Person("undefined", default);
      }
    }
  }
}