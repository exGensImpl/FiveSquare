using System.Threading;
using WPFLocalizeExtension.Engine;

namespace ExGens.FiveSquare
{
  /// <summary>
  /// Логика взаимодействия для App.xaml
  /// </summary>
  public partial class App
  {
    public App()
    {
      LocalizeDictionary.Instance.Culture = Thread.CurrentThread.CurrentCulture;
    }
  }
}
