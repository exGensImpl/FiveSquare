using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents an amount of visits of the specified venue
  /// </summary>
  internal readonly struct Visit
  {
    /// <summary>
    /// Venue which has been visited
    /// </summary>
    public Place Venue { get; }

    /// <summary>
    /// Amount of visits
    /// </summary>
    public int Times { get; }

    public Visit(Place venue, int times)
    {
      Venue = venue;
      Times = times;
    }

    /// <inheritdoc />
    public override string ToString() => $"{Times} times in {Venue}";
  }
}