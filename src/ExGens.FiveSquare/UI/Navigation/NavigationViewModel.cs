using System;
using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Services;
using ExGens.FiveSquare.UI.Navigation.Auth;
using ExGens.FiveSquare.UI.Navigation.Map;
using ExGens.FiveSquare.UI.Navigation.Stats;

namespace ExGens.FiveSquare.UI.Navigation
{
  internal sealed class NavigationViewModel : NotifyPropertyChangedTrait, IViewModel
  {

    public IReadOnlyList<IModeFactory> Modes { get; } = new IModeFactory[]
    {
      new MapMode(),
      new StatsMode(),
      new AuthMode(), 
    };

    public IModeFactory SelectedMode
    {
      get => m_selectedMode;
      set => OnPropertyChanged(ref m_selectedMode, value);
    }

    private IModeFactory m_selectedMode;

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
