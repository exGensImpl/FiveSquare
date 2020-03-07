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
    /// The place address
    /// </summary>
    public Address Address { get; }

    /// <summary>
    /// Categories that the place belongs to
    /// </summary>
    public IReadOnlyCollection<Category> Categories { get; }

    public Place(string name, Address address, IReadOnlyCollection<Category> categories)
    {
      Name = name;
      Address = address;
      Categories = categories;
    }

    /// <inheritdoc />
    public override string ToString() => Name;
  }
}