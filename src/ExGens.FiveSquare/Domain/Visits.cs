namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents an amount of visits of the specified place
  /// </summary>
  internal readonly struct Visits<T>
  {
    /// <summary>
    /// Place which has been visited
    /// </summary>
    public T Place { get; }

    /// <summary>
    /// Amount of visits
    /// </summary>
    public int Times { get; }

    public Visits(T place, int times)
    {
      Place = place;
      Times = times;
    }

    /// <inheritdoc />
    public override string ToString() => $"{Times} times in {Place}";
  }
}