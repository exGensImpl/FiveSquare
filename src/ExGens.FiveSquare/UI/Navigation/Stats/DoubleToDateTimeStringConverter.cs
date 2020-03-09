using System;
using System.Windows.Data;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  public class DoubleToDateTimeStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return value is double d ? 
        (object) new DateTime((long) d).ToString("dd MMMM yy HH:00") : 
        DateTime.Now;
    }
 
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}