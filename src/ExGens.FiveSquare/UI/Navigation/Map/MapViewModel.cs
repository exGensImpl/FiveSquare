using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Infrastructure;
using ExGens.FiveSquare.Services;
using ExGens.FiveSquare.UI.Navigation.Map.Layers;
using Mapsui.Layers;
using Microsoft.Expression.Interactivity.Core;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class MapViewModel : NotifyPropertyChangedTrait, IViewModel
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

      CategoryStats.FromVisits(services.FiveSquare.GetVisits().ToEnumerable())
                   .OrderByDescending(_ => _.Visits).ThenBy(_ => _.Category.Name)
                   .Select(_ => new CategoryModel(_.Category, _.Visits))
                   .Foreach(Categories.Add);

      Categories.ListChanged += CategoriesChanged;
      UpdateCheckins();
    }

    private void DoUncheckAllCategories()
    {
      Categories.ListChanged -= CategoriesChanged;
      Categories.Foreach(_ => _.Selected = false);
      Categories.ListChanged += CategoriesChanged;

      UpdateCheckins();
    }

    private void CategoriesChanged(object sender, ListChangedEventArgs e) => UpdateCheckins();

    private void UpdateCheckins()
    {
      var selected = Categories.Where(_ => _.Selected).Select(_ => _.Category).ToArray();
      m_services.FiveSquare.GetVisits()
                .Where(_ => _.Venue.Categories.Intersect(selected).Any())
                .To(m_factory.UpdateCheckins);
    }
  }
}