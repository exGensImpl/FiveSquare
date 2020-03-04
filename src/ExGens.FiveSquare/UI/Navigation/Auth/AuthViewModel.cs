using CefSharp;
using CefSharp.Wpf;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation.Auth
{
  internal sealed class AuthViewModel : ViewModelBase
  {
    public string AuthUrl { get; }

    public string AddressString
    {
      set
      {
        if (m_services.TokenFactory.TrySetAuthTokenFromUrl(value))
        {
          m_services.NavigateHome();
        }
      }
    }

    private readonly FiveSquareServices m_services;

    static AuthViewModel()
    {
      var settings = new CefSettings
      {
        CachePath = "cache"
      };

      settings.CefCommandLineArgs.Add("no-proxy-server");
      Cef.Initialize(settings);
    }

    public AuthViewModel(FiveSquareServices services)
    {
      m_services = services;
      AuthUrl = services.TokenFactory.GetAuthUri();
    }
  }
}
