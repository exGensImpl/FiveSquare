using System;
using System.Globalization;
using System.Linq;

namespace ExGens.FiveSquare.Domain
{
  /// <summary>
  /// Represents a country
  /// </summary>
  internal readonly struct Country : IEquatable<Country>
  {
    /// <summary>
    /// The county ISO-3166-1 alpha 2 code
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Country name
    /// </summary>
    public string Name { get; }

    public Country(string name)
    {
      var country = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                               .Select(_ => new RegionInfo(_.LCID))
                               .FirstOrDefault( _ => _.EnglishName == name);
      
      Name = country?.DisplayName ?? name;
      Code = country?.TwoLetterISORegionName ?? "";
    }

    #region Equality
    
    /// <inheritdoc />
    public bool Equals(Country other)
    {
      return Code == other.Code;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return obj is Country other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return (Code != null ? Code.GetHashCode() : 0);
    }

    public static bool operator ==(Country left, Country right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(Country left, Country right)
    {
      return !left.Equals(right);
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() => Name;
  }
}