using System;
using System.Windows.Data;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  public class DoubleToDateTimeStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return new DateTime((long)(double)value).ToString( "dd MMMM yy HH:00");
    }
 
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}