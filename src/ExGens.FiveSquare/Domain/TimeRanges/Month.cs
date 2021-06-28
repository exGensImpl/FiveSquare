using System;

namespace ExGens.FiveSquare.Domain.TimeRanges
{
  /// <summary>
  /// Represents a date and time up to month
  /// </summary>
  internal readonly struct Month : ITimeRange
  {
    /// <inheritdoc />
    public DateTime Start { get; }

    /// <inheritdoc />
    public string ShortDescription => $"{Start:Y}";

    /// <inheritdoc />
    public string LongDescription => $"{Start:Y}";

    public Month(DateTime start)
    {
      Start = start.Date.Subtract(TimeSpan.FromDays(start.Day - 1));
    }

    /// <inheritdoc />
    public ITimeRange GetNext()
    {
      var res = Start.AddMonths(1);
      var res2 = new Month(res);
      return res2;
    }

    /// <inheritdoc />
    public bool Equals(ITimeRange other) => other is Month month && Start == month.Start;

    /// <inheritdoc />
    public int CompareTo(ITimeRange other) => Start.CompareTo(other.Start);

    /// <inheritdoc />
    public override string ToString() => ShortDescription;
  }
}