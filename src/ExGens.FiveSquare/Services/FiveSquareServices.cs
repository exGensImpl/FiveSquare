using System;
using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquareServices
  {
    public FiveSquare FiveSquare { get; private set; }

    public IFourSquareAccessTokenFactory TokenFactory { get; }

    private readonly Action m_navigateHome;

    public FiveSquareServices(Action navigateHome)
    {
      m_navigateHome = navigateHome;

      var appInfo = FourSquareApplicationInfo.Current;
      var client = new SharpSquare(appInfo.ClientId, appInfo.ClientSecret);

      FiveSquare = new FiveSquare(client);
      TokenFactory = new ChromeFourSquareAccessTokenFactory(client, appInfo.Redirect);
    }

    public void NavigateHome() => m_navigateHome();
  }
}
