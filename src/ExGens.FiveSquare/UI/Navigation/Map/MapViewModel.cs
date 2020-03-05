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

    public BindingList<CategoryModel> Categories { get; } = new BindingList<CategoryModel>();

    private readonly FiveSquareServices m_services;
    private readonly LayerFactory m_factory;

    public MapViewModel(FiveSquareServices services)
    {
      UncheckAllCategories = new ActionCommand(DoUncheckAllCategories);

      m_services = services;
      m_factory = new LayerFactory(LayerSettings.Default);

      User = services.FiveSquare.User;
      Layers = m_factory.Layers;

      var categories =
        from visit in services.FiveSquare.GetVisits()
        from category in visit.Venue.Categories
        group visit by category;

      foreach (var category in categories.OrderByDescending(_ => _.Sum(v => v.Times)).ThenBy(_ => _.Key.Name))
      {
        Categories.Add(new CategoryModel(category.Key, category.Sum(v => v.Times)));
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
      var selectedCategories = Categories.Where(_ => _.Selected).Select(_ => _.Category);
      
      var checkinsToShow = 
        from visit in m_services.FiveSquare.GetVisits()
        from category in selectedCategories
        where visit.Venue.Categories.Contains(category)
        select visit;

      m_factory.UpdateCheckins(checkinsToShow.ToArray( ));
    }
  }
}