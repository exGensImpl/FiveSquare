using System;
using System.Windows.Data;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  public class DateTimeDoubleConverter : IValueConverter
  {
    public bool Inverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return Inverse ? (object)ToDateTime(value) : ToDouble(value);
    }
 
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return Inverse ? (object)ToDouble(value) : ToDateTime(value);
    }

    private static double ToDouble(object value)
    {
      return value is DateTime dateTime?
        dateTime.Ticks : DateTime.Now.Ticks;
    }

    private static DateTime ToDateTime(object value)
    {
      return value is double d?
        new DateTime((long)d) : DateTime.Now;
    }
  }
}