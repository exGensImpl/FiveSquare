using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Interactivity;
using CefSharp;
using CefSharp.Wpf;

namespace ExGens.FiveSquare.UI.Navigation.Auth
{
  internal sealed class AddressStringBinding : Behavior<ChromiumWebBrowser>
  {
    #region LayerSource
    
    public string AddressString
    {
      get => (string)GetValue(AddressStringProperty);
      set => Dispatcher?.Invoke(() => SetValue(AddressStringProperty, value));
    }

    public static readonly DependencyProperty AddressStringProperty = DependencyProperty.Register(
      name: nameof(AddressString),
      propertyType: typeof(string),
      ownerType: typeof(AddressStringBinding) );

    #endregion

    protected override void OnAttached()
    {
      AssociatedObject.RequestHandler = new BeforeBrowseRequestHandler(
        request => AddressString = request.Url);
    }

    private class BeforeBrowseRequestHandler : IRequestHandler
    {
      private readonly Action<IRequest> m_beforeBrowseAction;

      public BeforeBrowseRequestHandler(Action<IRequest> beforeBrowseAction)
      {
        m_beforeBrowseAction = beforeBrowseAction;
      }

      /// <inheritdoc />
      public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture,
        bool isRedirect)
      {
        m_beforeBrowseAction(request);
        return false;
      }

      /// <inheritdoc />
      public bool OnOpenUrlFromTab(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl,
        WindowOpenDisposition targetDisposition, bool userGesture)
      {
        return false;
      }

      /// <inheritdoc />
      public IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame,
        IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
      {
        return null;
      }

      /// <inheritdoc />
      public bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host,
        int port, string realm, string scheme, IAuthCallback callback)
      {
        return false;
      }

      /// <inheritdoc />
      public bool OnQuotaRequest(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, long newSize,
        IRequestCallback callback)
      {
        return false;
      }

      /// <inheritdoc />
      public bool OnCertificateError(IWebBrowser chromiumWebBrowser, IBrowser browser, CefErrorCode errorCode, string requestUrl,
        ISslInfo sslInfo, IRequestCallback callback)
      {
        return false;
      }

      /// <inheritdoc />
      public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port,
        X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
      {
        return false;
      }

      /// <inheritdoc />
      public void OnPluginCrashed(IWebBrowser chromiumWebBrowser, IBrowser browser, string pluginPath)
      {
      }

      /// <inheritdoc />
      public void OnRenderViewReady(IWebBrowser chromiumWebBrowser, IBrowser browser)
      {
      }

      /// <inheritdoc />
      public void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
      {
      }
    }
  }
}