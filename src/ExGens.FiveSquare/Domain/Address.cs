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
    public Country Country { get; }

    /// <summary>
    /// Coutry of the place described
    /// </summary>
    public string City { get; }
    
    /// <summary>
    /// Coordinates of the place described
    /// </summary>
    public Coordinates Location { get; }

    public Address(string s, Country country, string city, Coordinates location)
    {
      String = s;
      Country = country;
      City = city;
      Location = location;
    }

    /// <inheritdoc />
    public override string ToString() => $"{String} {Location}";
  }
}