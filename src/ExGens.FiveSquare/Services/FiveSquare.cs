using System;
using System.Collections.Generic;
using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquare
  {
    public Person User { get; } = new Person("", new Coordinates(59.94, 30.3));

    public IReadOnlyList<Visit> GetVisits() => Array.Empty<Visit>();
  }
}