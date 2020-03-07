namespace ExGens.FiveSquare.Services
{
  internal interface IFourSquareAccessTokenFactory
  {
    /// <summary>
    /// Returns an URI which allows to request a FourSquare authorization code
    /// </summary>
    /// <returns></returns>
    /// <exception cref="AuthorizationException"></exception>
    string GetAuthUri();

    /// <summary>
    /// Tries to parse the specified URL and requests a FourSquare access token
    /// by extracted auth code. Sets the token to the client if the request was successful
    /// </summary>
    /// <param name="url">URL to parse</param>
    /// <returns>
    /// <see langword="true"/> if the URL contains a valid code that allows to get access token,
    /// otherwise <see langword="false"/>
    /// </returns>
    /// <exception cref="AuthorizationException"></exception>
    bool TrySetAuthTokenFromUrl(string url);
  }
}