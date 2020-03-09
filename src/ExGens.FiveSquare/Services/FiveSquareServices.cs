using System;
using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquareServices
  {
    public FiveSquare FiveSquare { get; }

    public IFourSquareAccessTokenFactory TokenFactory { get; }

    private readonly Action m_navigateHome;

    public FiveSquareServices(Action navigateHome)
    {
      m_navigateHome = navigateHome;

      var appInfo = FourSquareApplicationInfo.Current;
      var client = new SharpSquare(appInfo.ClientId, appInfo.ClientSecret);

      FiveSquare = new FiveSquare(client);
      TokenFactory = appInfo.AccessToken == null?
        new ChromeFourSquareAccessTokenFactory(client, appInfo.Redirect) :
        (IFourSquareAccessTokenFactory)new DirectTokenFactory(client, appInfo.AccessToken); 
    }

    public void NavigateHome() => m_navigateHome();
  }
}
