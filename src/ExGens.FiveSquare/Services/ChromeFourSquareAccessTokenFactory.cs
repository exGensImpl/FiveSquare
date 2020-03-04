using System;
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
      return m_client.GetAuthenticateUrl(m_redirect);
    }

    /// <inheritdoc />
    public bool TrySetAuthTokenFromUrl(string url)
    {
      if (url?.Contains("?code=") != true)
      {
        return false;
      }
      
      var i = url.LastIndexOf("?code=", StringComparison.InvariantCultureIgnoreCase) + 6;
      var code = url.Substring(i, url.Length - i);
      var token = m_client.GetAccessToken(m_redirect, code);
      m_client.SetAccessToken(token);
      return true;
    }
  }
}