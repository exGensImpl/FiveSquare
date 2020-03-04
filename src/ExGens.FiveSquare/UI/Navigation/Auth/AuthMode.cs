using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation.Auth
{
  internal sealed class AuthMode : IModeFactory
  {
    /// <inheritdoc />
    public string Title => "Auth";

    /// <inheritdoc />
    public Type ViewType => typeof(AuthView);

    /// <inheritdoc />
    public FiveSquareServices Services { get; set; }

    /// <inheritdoc />
    public ViewModelBase CreateViewModel()
    {
      return new AuthViewModel(Services);
    }
  }
}
