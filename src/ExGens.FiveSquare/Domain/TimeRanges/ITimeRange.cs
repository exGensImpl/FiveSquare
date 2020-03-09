using System;

namespace ExGens.FiveSquare.Domain.TimeRanges
{
  /// <summary>
  /// Represents a time range
  /// </summary>
  internal interface ITimeRange : IEquatable<ITimeRange>, IComparable<ITimeRange>
  {
    /// <summary>
    /// Start date of the time range
    /// </summary>
    DateTime Start { get; }

    /// <summary>
    /// Time range short description
    /// </summary>
    string ShortDescription { get; }

    /// <summary>
    /// Time range long description
    /// </summary>
    string LongDescription { get; }

    /// <summary>
    /// Returns the next time range of the current type
    /// </summary>
    ITimeRange GetNext();
  }
}