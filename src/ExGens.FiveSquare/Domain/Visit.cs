using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents an amount of visits of the specified location
  /// </summary>
  internal readonly struct Visit
  {
    /// <summary>
    /// Location of the visited place
    /// </summary>
    public Coordinates Location { get; }

    /// <summary>
    /// Amount of visits
    /// </summary>
    public int Times { get; }

    public Visit(Coordinates location, int times)
    {
      Location = location;
      Times = times;
    }

    /// <inheritdoc />
    public override string ToString() => $"{Times} times in {Location}";
  }
}