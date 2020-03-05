using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Services;
using ExGens.FiveSquare.UI.Navigation.Map.Layers;
using Mapsui.Layers;
using Microsoft.Expression.Interactivity.Core;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class MapViewModel : ViewModelBase
  {
    public ICommand UncheckAllCategories { get; }

    public Person User { get; }

    public IEnumerable<ILayer> Layers { get; }

    public Coordinates Location
    {
      get => m_location;
      set => OnPropertyChanged(ref m_location, value);
    }

    public BindingList<CategoryModel> Categories { get; } = new BindingList<CategoryModel>();

    private readonly FiveSquareServices m_services;
    private Coordinates m_location;
    private readonly LayerFactory m_factory;

    public MapViewModel(FiveSquareServices services)
    {
      UncheckAllCategories = new ActionCommand(DoUncheckAllCategories);

      m_services = services;
      m_factory = new LayerFactory(LayerSettings.Default);

      User = services.FiveSquare.User;
      Location = User.Home;
      Layers = m_factory.Layers;
      
      var chekins = services.FiveSquare.GetVisits();

      foreach (var category in chekins.SelectMany(_ => _.Venue.Categories).Distinct())
      {
        Categories.Add(new CategoryModel(category));
      }

      Categories.ListChanged += CategoriesChanged;
      UpdateCheckins();
    }

    private void DoUncheckAllCategories()
    {
      Categories.ListChanged -= CategoriesChanged;
      foreach (var category in Categories)
      {
        category.Selected = false;
      }
      Categories.ListChanged += CategoriesChanged;
      UpdateCheckins();
    }

    private void CategoriesChanged(object sender, ListChangedEventArgs e) => UpdateCheckins();

    private void UpdateCheckins()
    {
      var selectedCategories = Categories.Where(_ => _.Selected)
        .Select(_ => _.Category)
        .ToArray();

      var checkinsToShow = m_services.FiveSquare.GetVisits()
        .Where(_ => _.Venue.Categories.Intersect(selectedCategories).Any())
        .ToArray();

      m_factory.UpdateCheckins(checkinsToShow);
    }
  }
}