using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ExGens.FiveSquare.Domain;
using FourSquare.SharpSquare.Core;

namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquare
  {
    public Person User { get; } = new Person("", new Coordinates(59.94, 30.3));

    public IEnumerable<Visit> GetVisits(string userID = null) => Array.Empty<Visit>();
  }
}