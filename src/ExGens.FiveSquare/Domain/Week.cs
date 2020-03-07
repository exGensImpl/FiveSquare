using System;
using System.Globalization;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents a week
  /// </summary>
  internal readonly struct Week : IEquatable<Week>, IComparable<Week>
  {
    /// <summary>
    /// First day of the week
    /// </summary>
    public DateTime Start { get; }

    /// <summary>
    /// Last day of the week
    /// </summary>
    public DateTime End { get; }

    /// <summary>
    /// Year of the week
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// Week number respect to its year
    /// </summary>
    public int WeekNumber { get; }

    /// <summary>
    /// Returns string representation of the week with year and week number like 2020, 3
    /// </summary>
    public string WeekNumberString => $"{Year}, {WeekNumber}";
    
    /// <summary>
    /// Returns string representation of the week with its start and end like 02.03.2020 - 08.03.2020
    /// </summary>
    public string StartEndString => $"{Start:d} - {End:d}";
    
    /// <summary>
    /// Returns string representation of the week with month and year like march, 2020 or december, 2019 - january, 2020
    /// </summary>
    public string Months
    {
      get
      {
        var start = Start.ToString("Y");
        var end = End.ToString("Y");
        return start == end? start : $"{start} - {end}";
      }
    }

    public Week(DateTime start)
    {
      Start = FirstDayOfWeek(start);
      End = Start.AddDays(6);
      Year = Start.Year;
      WeekNumber = CultureInfo.CurrentCulture
        .Calendar
        .GetWeekOfYear(start, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }

    private static DateTime FirstDayOfWeek(DateTime date)
    {
      return date.AddDays(-Mod(date.DayOfWeek - DayOfWeek.Monday, 7)).Date;

      int Mod(int k, int n) => (k %= n) < 0? k+n : k;
    }

    /// <inheritdoc />
    public bool Equals(Week other)
    {
      return Start == other.Start;
    }

    /// <inheritdoc />
    public int CompareTo(Week other)
    {
      return Start.CompareTo(other.Start);
    }

    /// <inheritdoc />
    public override string ToString() => StartEndString;
  }
}