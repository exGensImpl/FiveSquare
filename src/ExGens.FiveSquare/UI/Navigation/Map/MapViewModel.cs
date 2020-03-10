using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Infrastructure;
using ExGens.FiveSquare.Properties;
using ExGens.FiveSquare.Services;
using ExGens.FiveSquare.UI.Navigation.Map.Layers;
using Mapsui.Layers;
using Microsoft.Expression.Interactivity.Core;
using ReactiveUI;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class MapViewModel : CheckinRangeBasedViewModel
  {
    public ICommand UncheckAllCategories { get; }

    public Coordinates Location
    {
      get => m_location;
      private set => this.RaiseAndSetIfChanged(ref m_location, value);
    }

    public IEnumerable<ILayer> Layers => m_factory.Layers;

    public LayerSettings Settings { get; }

    public IReadOnlyCollection<Tuple<string, MetricFactory>> Metrics { get; }
      = new[]
      {
        Tuple.Create(Resources.MapView_Metrics_Linear, (MetricFactory)((v, m) => new LinearVisitCountMetric(v, m))),
        Tuple.Create(Resources.MapView_Metrics_Log, (MetricFactory)((v, m) => new LogVisitCountMetric(v, m))),
        Tuple.Create(Resources.MapView_Metrics_Const, (MetricFactory)((v, m) => new ConstantMetric(1.5f)))
      };

    public BindingList<CategoryModel> Categories { get; } = new BindingList<CategoryModel>();

    private readonly LayerFactory m_factory;
    private Coordinates m_location;

    public MapViewModel(FiveSquareServices services) : base(services)
    {
      UncheckAllCategories = new ActionCommand(DoUncheckAllCategories);

      m_factory = new LayerFactory(Settings = LayerSettings.Default);

      Categories.ListChanged += CategoriesChanged;
      Settings.PropertyChanged += (o, e) => UpdateCheckins();

      var rangeChanging = this.WhenAnyValue(_ => _.Start, _ => _.End)
                              .Throttle(TimeSpan.FromSeconds(0.5), RxApp.TaskpoolScheduler);
      
      rangeChanging.Select(_ => GetCategoryModels())
                   .ObserveOn(RxApp.MainThreadScheduler)
                   .Subscribe(UpdateCategoryModels);
        
      rangeChanging.Select(_ => GetSelectedCheckins())
                   .ObserveOn(RxApp.MainThreadScheduler)
                   .Subscribe(m_factory.UpdateCheckins);

      services.FiveSquare.GetCheckins().Take(1)
                         .Subscribe(_ => Location = _.Location.Address.Location);
    }
    
    private void UpdateCategoryModels(IReadOnlyCollection<CategoryModel> models)
    {
      Categories.ListChanged -= CategoriesChanged;

      models.Foreach(_ => _.Selected = Categories.FirstOrDefault(c => c.Category == _.Category)?.Selected != false);

      Categories.Clear();
      models.Foreach(Categories.Add);

      Categories.ListChanged += CategoriesChanged;
      CategoriesChanged(Categories, new ListChangedEventArgs(ListChangedType.Reset, 0));
    }

    private IReadOnlyCollection<CategoryModel> GetCategoryModels()
      => CategoryStats.FromVisits(GetFilteredVisits().ToEnumerable())
                      .OrderByDescending(_ => _.Visits).ThenBy(_ => _.Category.Name)
                      .Select(_ => new CategoryModel(_.Category, _.Visits))
                      .ToArray();

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
      m_factory.UpdateCheckins(GetSelectedCheckins());
    }
    
    private IObservable<Visit> GetSelectedCheckins()
    {
      var selected = Categories.Where(_ => _.Selected).Select(_ => _.Category).ToArray();
      return GetFilteredVisits().Where(_ => _.Venue.Categories.Intersect(selected).Any());
    }

    private IObservable<Visit> GetFilteredVisits()
      => GetFilteredCheckins()
        .GroupBy(_ => _.Location)
        .SelectAsync(async _ => new Visit(_.Key, await _.Count()));
  }
}