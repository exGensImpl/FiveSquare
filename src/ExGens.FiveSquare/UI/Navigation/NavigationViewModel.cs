using System;
using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Services;
using ExGens.FiveSquare.UI.Navigation.Auth;
using ExGens.FiveSquare.UI.Navigation.Map;

namespace ExGens.FiveSquare.UI.Navigation
{
  internal sealed class NavigationViewModel : ViewModelBase
  {

    public IReadOnlyList<IModeFactory> Modes { get; } = new IModeFactory[]
    {
      new MapMode(),
      new AuthMode(), 
    };

    public IModeFactory SelectedMode
    {
      get => _selectedMode;
      set => OnPropertyChanged(ref _selectedMode, value);
    }

    private IModeFactory _selectedMode;

    public NavigationViewModel()
    {
      var services = new FiveSquareServices(() => SelectedMode = Modes[0]);

      foreach (var mode in Modes)
      {
        mode.Services = services;
      }

      SelectedMode = Modes.OfType<AuthMode>().Single();
    }
  }
}
