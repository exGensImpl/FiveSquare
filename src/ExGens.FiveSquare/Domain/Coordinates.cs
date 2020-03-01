namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Geographical coordinates represented in latitude and longitude
  /// </summary>
  internal readonly struct Coordinates
  {
    /// <summary>
    /// Latitude
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// Longitude
    /// </summary>
    public double Longitude { get; }

    public Coordinates(double latitude, double longitude)
    {
      Latitude = latitude;
      Longitude = longitude;
    }

    /// <inheritdoc />
    public override string ToString() => $"({Latitude}, {Longitude})";
  }
}