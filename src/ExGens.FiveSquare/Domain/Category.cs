using System.Collections.Concurrent;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents a place category
  /// </summary>
  internal sealed class Category
  {
    private static readonly ConcurrentDictionary<string, Category> Cache
      = new ConcurrentDictionary<string, Category>();

    /// <summary>
    /// Identificator of the category
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Category name
    /// </summary>
    public string Name { get; }

    public static Category GetOrCreate(string id, string name)
    {
      return Cache.GetOrAdd(id, new Category(id, name));
    }

    private Category(string id, string name)
    {
      Id = id;
      Name = name;
    }

    /// <inheritdoc />
    public override string ToString() => Name;
  }
}