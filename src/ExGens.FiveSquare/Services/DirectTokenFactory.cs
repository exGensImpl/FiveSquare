using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class DirectTokenFactory : IFourSquareAccessTokenFactory
  {
    private readonly SharpSquare m_client;

    private readonly string m_token;

    public DirectTokenFactory(SharpSquare client, string token)
    {
      m_client = client;
      m_token = token;
    }

    /// <inheritdoc />
    public string GetAuthUri() => "";

    /// <inheritdoc />
    public bool TrySetAuthTokenFromUrl(string url)
    {
      m_client.SetAccessToken(m_token);
      return true;
    }
  }
}