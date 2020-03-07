namespace ExGens.FiveSquare.Services
{
  internal readonly struct FourSquareApplicationInfo
  {
    public static FourSquareApplicationInfo Current
      => new FourSquareApplicationInfo(
        "FXYEN3JAZUMRV4FXYGH2TZYWJ2HUDEM1ZCYTOEVIQYXXO2IO",
        "CU50513WOO0UAR0TVV2P0GCJF0PYJAMFAKH20OARUMOKRVZV",
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