using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represent a person
  /// </summary>
  internal readonly struct Person
  {
    /// <summary>
    /// Name of the person
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Home location of the person
    /// </summary>
    public Coordinates Home { get; }

    public Person(string name, Coordinates home)
    {
      Name = name;
      Home = home;
    }

    /// <inheritdoc />
    public override string ToString() => Name;
  }
}