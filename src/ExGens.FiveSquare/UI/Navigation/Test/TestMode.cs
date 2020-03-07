using System;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation.Test
{
  internal sealed class TestMode : IModeFactory
  {
    /// <inheritdoc />
    public Type ViewType => typeof(TestView);

    /// <inheritdoc />
    public FiveSquareServices Services { get; set; }

    public TestMode(string title)
    {
      Title = title;
    }

    /// <inheritdoc />
    public string Title { get; }
  
    /// <inheritdoc />
    public IViewModel CreateViewModel()
    {
      return null;
    }
  }
}