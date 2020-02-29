using System;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation
{
  internal interface IModeFactory
  {
    string Title { get; }

    Type ViewType { get; }

    FiveSquareServices Services { set; }

    ViewModelBase CreateViewModel();
  }
}
