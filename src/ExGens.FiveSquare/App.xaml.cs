using System.Threading;
using System.Windows;
using WPFLocalizeExtension.Engine;

namespace ExGens.FiveSquare
{
  /// <summary>
  /// Логика взаимодействия для App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      LocalizeDictionary.Instance.Culture = Thread.CurrentThread.CurrentCulture;
    }
  }
}
