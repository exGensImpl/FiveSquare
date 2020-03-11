using System;
using System.Collections.Generic;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents a venue
  /// </summary>
  internal readonly struct Venue : IEquatable<Venue>
  {
    /// <summary>
    /// The venue name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The venue address
    /// </summary>
    public Address Address { get; }

    /// <summary>
    /// Categories that the venue belongs to
    /// </summary>
    public IReadOnlyCollection<Category> Categories { get; }

    private readonly string m_id;

    public Venue(string id, string name, Address address, IReadOnlyCollection<Category> categories)
    {
      m_id = id;
      Name = name;
      Address = address;
      Categories = categories;
    }

    /// <inheritdoc />
    public override bool Equals(object other) => other is Venue && m_id.Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => m_id.GetHashCode();

    /// <inheritdoc />
    public bool Equals(Venue other) => m_id == other.m_id;


    /// <inheritdoc />
    public override string ToString() => Name;
  }
}