using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquare
  {
    private const string ClientId = "FXYEN3JAZUMRV4FXYGH2TZYWJ2HUDEM1ZCYTOEVIQYXXO2IO";
    private const string ClientSecret = "3RNFVFU3VDJEOLUJPBQ3YI3YWIIJG5ETZHRSABYKLM3SBQVF";
    private const string Redirect = "https://fakeuri";

    private readonly SharpSquare m_client;

    public Person User { get; }
    
    public IEnumerable<Visit> GetVisits()
      => m_client.GetUserVenueHistory().Select(_ => _.ToVisit());

    public FiveSquare(string accessToken)
    {
      m_client = new SharpSquare(ClientId, ClientSecret, accessToken);
      User = GetCurrentUserInfo();
    }

    private Person GetCurrentUserInfo()
    {
      var user = m_client.GetUser("self");
      var lastCheckinLocation = user.checkins.items.FirstOrDefault()?.venue?.location;
      return new Person(
        user.firstName,
        lastCheckinLocation?.ToCoordinates() ?? default);
    }
  }
}