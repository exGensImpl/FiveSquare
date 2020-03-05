using System.Collections.Generic;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents a place
  /// </summary>
  internal readonly struct Place
  {
    /// <summary>
    /// The place name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The place location
    /// </summary>
    public Coordinates Location { get; }

    /// <summary>
    /// Categories that the place belongs to
    /// </summary>
    public IReadOnlyCollection<Category> Categories { get; }

    public Place(string name, Coordinates location, IReadOnlyCollection<Category> categories)
    {
      Name = name;
      Location = location;
      Categories = categories;
    }

    /// <inheritdoc />
    public override string ToString() => Name;
  }
}