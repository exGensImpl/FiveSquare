namespace ExGens.FiveSquare.Services
{
  internal interface IFourSquareAccessTokenFactory
  {
    string GetAuthUri();

    bool TrySetAuthTokenFromUrl(string url);
  }
}