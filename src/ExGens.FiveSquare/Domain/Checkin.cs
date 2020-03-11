using System;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents a check-in
  /// </summary>
  internal readonly struct Checkin : IHaveDate
  {
    /// <summary>
    /// Date and time of the check-in
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Check-in location
    /// </summary>
    public Venue Location { get; }

    public Checkin(DateTime date, Venue location)
    {
      Date = date;
      Location = location;
    }

    /// <inheritdoc />
    public override string ToString() => $":{Date} in {Location}";
  }
}