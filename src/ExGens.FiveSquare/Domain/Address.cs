namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents an address description
  /// </summary>
  internal readonly struct Address
  {
    /// <summary>
    /// String representation with city, street, biulding number, etc.
    /// </summary>
    public string String { get; }

    /// <summary>
    /// Coutry of the place described
    /// </summary>
    public string Country { get; }
    
    /// <summary>
    /// Coordinates of the place described
    /// </summary>
    public Coordinates Location { get; }

    public Address(string s, string country, Coordinates location)
    {
      String = s;
      Country = country;
      Location = location;
    }

    /// <inheritdoc />
    public override string ToString() => $"{String} {Location}";
  }
}