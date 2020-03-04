namespace ExGens.FiveSquare.Services
{
  internal readonly struct FourSquareApplicationInfo
  {
    public static FourSquareApplicationInfo Current
      => new FourSquareApplicationInfo(
        "FXYEN3JAZUMRV4FXYGH2TZYWJ2HUDEM1ZCYTOEVIQYXXO2IO",
        "/*privacy*/",
        "https://fakeuri");

    public string ClientId { get; }
    public string ClientSecret { get; }
    public string Redirect { get; }

    public FourSquareApplicationInfo(string clientId, string clientSecret, string redirect)
    {
      ClientId = clientId;
      ClientSecret = clientSecret;
      Redirect = redirect;
    }
  }
}