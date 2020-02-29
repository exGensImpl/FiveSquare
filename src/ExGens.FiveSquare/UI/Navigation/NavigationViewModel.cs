using System;
using System.Collections.Generic;
using ExGens.FiveSquare.UI.Navigation.Test;

namespace ExGens.FiveSquare.UI.Navigation
{
  internal sealed class NavigationViewModel : ViewModelBase
  {
    public IReadOnlyList<IModeFactory> Modes { get; } = new[] {new TestMode("First mode"), new TestMode("Second mode"), };
  }
}
