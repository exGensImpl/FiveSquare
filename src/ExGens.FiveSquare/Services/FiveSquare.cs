using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquare
  {
    private readonly SharpSquare m_client;
    private Person? m_userCache;
    private IReadOnlyList<Visit> m_visitCache;

    public Person User => m_userCache ?? (m_userCache = GetCurrentUserInfo()).Value;
    
    public IReadOnlyList<Visit> GetVisits()
    {
      try
      {
        return m_visitCache ?? (m_visitCache = m_client.GetUserVenueHistory().Select(_ => _.ToVisit()).ToArray());
      }
      catch(WebException)
      {
        return Array.Empty<Visit>();
      }
    }

    public FiveSquare(SharpSquare client)
    {
      m_client = client;
    }

    public void ResetCaches()
    {
      m_userCache = null;
      m_visitCache = null;
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
      catch(WebException)
      {
        return new Person("undefined", default);
      }
    }
  }
}