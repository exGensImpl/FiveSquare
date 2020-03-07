using System;
using System.Collections.Generic;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents a place
  /// </summary>
  internal readonly struct Place : IEquatable<Place>
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

    private readonly string m_id;

    public Place(string id, string name, Address address, IReadOnlyCollection<Category> categories)
    {
      m_id = id;
      Name = name;
      Address = address;
      Categories = categories;
    }

    /// <inheritdoc />
    public override bool Equals(object other) => other is Place && m_id.Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => m_id.GetHashCode();

    /// <inheritdoc />
    public bool Equals(Place other) => m_id == other.m_id;


    /// <inheritdoc />
    public override string ToString() => Name;
  }
}