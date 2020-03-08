using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquare
  {
    private readonly SharpSquare m_client;
    private Person? m_userCache;
    private IReadOnlyList<Visit> m_visitCache;
    private readonly Dictionary<long,Checkin[]> m_checkinCache = new Dictionary<long, Checkin[]>();

    public Person User => m_userCache ?? (m_userCache = GetCurrentUserInfo()).Value;
    

    public FiveSquare(SharpSquare client)
    {
      m_client = client;
    }

    public IObservable<Visit> GetVisits()
      => SubjectExtensions.ColdObservable(() => m_visitCache ?? (m_visitCache = RequestVisits()));

    private Visit[] RequestVisits()
      => m_client.GetUserVenueHistory().Select(_ => _.ToVisit()).ToArray();

    public IObservable<Checkin> GetCheckins()
      => SubjectExtensions.ColdObservable<Checkin>(EmitCheckins);

    private void EmitCheckins(IObserver<Checkin> subject)
    {
      var limit = 250;
      var offset = 0;

      while(true)
      {
        if (m_checkinCache.ContainsKey(offset) == false)
        {
          var (_, success) = subject.Catch(
            () => m_checkinCache[offset] = RequestCheckins(offset, limit).Select(_ => _.ToCheckin()).ToArray()
          );

          if (success == false)
          {
            return;
          }
        }
        
        subject.EmitAll(m_checkinCache[offset]);

        if (m_checkinCache[offset].Length == 0)
        {
          return;
        }

        offset += m_checkinCache[offset].Length;
      }

      List<FourSquare.SharpSquare.Entities.Checkin> RequestCheckins(int skip, int batch)
        => m_client.GetUserCheckins(
          "self",
          new Dictionary<string, string>
          {
            ["limit"] = batch.ToString(),
            ["offset"] = skip.ToString()
          });
    }

    public void ResetCaches()
    {
      m_userCache = null;
      m_visitCache = null;
      m_checkinCache.Clear();
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