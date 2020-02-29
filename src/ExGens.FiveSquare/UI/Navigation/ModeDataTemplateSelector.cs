using System.Windows;
using System.Windows.Controls;

namespace ExGens.FiveSquare.UI.Navigation
{
  internal class ModeDataTemplateSelector : DataTemplateSelector
  {
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      if (!(item is IModeFactory factory)) return null;

      return new DataTemplate
      {
        VisualTree = new FrameworkElementFactory(factory.ViewType),
        Resources = new ResourceDictionary {{"VM", factory.CreateViewModel()}}
      };
    }
  }
}