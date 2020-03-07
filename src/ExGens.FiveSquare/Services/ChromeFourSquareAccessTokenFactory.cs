using System;
using System.Net;
using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class ChromeFourSquareAccessTokenFactory : IFourSquareAccessTokenFactory
  {
    private readonly SharpSquare m_client;
    private readonly string m_redirect;

    public ChromeFourSquareAccessTokenFactory(SharpSquare client, string redirect)
    {
      m_client = client;
      m_redirect = redirect;
    }

    /// <inheritdoc />
    public string GetAuthUri()
    {
      try
      {
        return m_client.GetAuthenticateUrl(m_redirect);
      }
      catch (WebException error)
      {
        throw new AuthorizationException("Cannot get authentication URL", error);
      }
    }

    /// <inheritdoc />
    public bool TrySetAuthTokenFromUrl(string url)
    {
      if (url?.Contains("?code=") != true)
      {
        return false;
      }

      try
      {
        var i = url.LastIndexOf("?code=", StringComparison.InvariantCultureIgnoreCase) + 6;
        var code = url.Substring(i, url.Length - i);
        var token = m_client.GetAccessToken(m_redirect, code);
        m_client.SetAccessToken(token);
        return true;
      }
      catch (WebException error)
      {
        throw new AuthorizationException("Cannot get or set an access token", error);
      }
    }
  }
}