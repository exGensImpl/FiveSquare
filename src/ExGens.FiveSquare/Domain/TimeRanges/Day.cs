using System;

namespace ExGens.FiveSquare.Domain.TimeRanges
{
  /// <summary>
  /// Represents a date and time up to day
  /// </summary>
  internal readonly struct Day : ITimeRange
  {
    /// <inheritdoc />
    public DateTime Start { get; }

    /// <inheritdoc />
    public string ShortDescription => $"{Start:d}";

    /// <inheritdoc />
    public string LongDescription => $"{Start:D}";

    public Day(DateTime start)
    {
      Start = start.Date;
    }

    /// <inheritdoc />
    public ITimeRange GetNext() => new Day(Start.AddDays(1));

    /// <inheritdoc />
    public bool Equals(ITimeRange other) => other is Day day && Start == day.Start;

    /// <inheritdoc />
    public int CompareTo(ITimeRange other) => Start.CompareTo(other.Start);

    /// <inheritdoc />
    public override string ToString() => ShortDescription;
  }
}