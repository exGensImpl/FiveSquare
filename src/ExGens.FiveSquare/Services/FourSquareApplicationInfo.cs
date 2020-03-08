using System.IO;

namespace ExGens.FiveSquare.Services
{
  internal readonly struct FourSquareApplicationInfo
  {
    public static FourSquareApplicationInfo Current
    {
      get
      {
        var data = File.ReadAllLines("key");
        return new FourSquareApplicationInfo(data[0], data[1], data[2]);
      }
    }

    public string ClientId { get; }
    public string ClientSecret { get; }
    public string Redirect { get; }

    private FourSquareApplicationInfo(string clientId, string clientSecret, string redirect)
    {
      ClientId = clientId;
      ClientSecret = clientSecret;
      Redirect = redirect;
    }
  }
}