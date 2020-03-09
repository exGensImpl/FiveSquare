using System;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents an entity with date
  /// </summary>
  internal interface IHaveDate
  {
    /// <summary>
    /// Date and time of the entity
    /// </summary>
    DateTime Date { get; }
  }
}