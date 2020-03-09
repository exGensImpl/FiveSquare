using System;

namespace ExGens.FiveSquare.Domain.TimeRanges
{
  /// <summary>
  /// Represents a date and time up to hour
  /// </summary>
  internal readonly struct Hour : ITimeRange
  {
    /// <inheritdoc />
    public DateTime Start { get; }

    /// <inheritdoc />
    public string ShortDescription => $"{Start:d MMMM H\\:00}";

    /// <inheritdoc />
    public string LongDescription => $"{Start:dddd, d MMMM HH\\:00}";

    public Hour(DateTime start)
    {
      Start = start.Subtract(start.TimeOfDay.Subtract(TimeSpan.FromHours(start.Hour)));
    }

    /// <inheritdoc />
    public ITimeRange GetNext() => new Hour(Start.AddHours(1));

    /// <inheritdoc />
    public bool Equals(ITimeRange other) => other is Hour hour && Start == hour.Start;

    /// <inheritdoc />
    public int CompareTo(ITimeRange other) => Start.CompareTo(other.Start);

    /// <inheritdoc />
    public override string ToString() => ShortDescription;
  }
}