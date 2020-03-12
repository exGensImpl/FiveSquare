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
      UncheckAllCategories = new ActionCommand(() => Categories.BatchForeach(_ => _.Selected = false));

      m_factory = new LayerFactory(Settings = LayerSettings.Default);

      Categories.ListChanged += (o, e) => m_factory.UpdateCheckins(GetSelectedCheckins());
      Settings.PropertyChanged += (o, e) => m_factory.UpdateCheckins(GetSelectedCheckins());

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
      models.Foreach(_ => _.Selected = Categories.FirstOrDefault(c => c.Category == _.Category)?.Selected != false);
      Categories.Replace(models);
    }

    private IReadOnlyCollection<CategoryModel> GetCategoryModels()
      => GetFilteredCheckins().ToEnumerable()
                              .VenueVisits()
                              .StatsBy(_ => _.Categories)
                              .OrderByDescending(_ => _.Visits)
                              .ThenBy(_ => _.Location.Name)
                              .Select(_ => new CategoryModel(_.Location, _.Visits))
                              .ToArray();
    
    private IObservable<Visits<Venue>> GetSelectedCheckins()
    {
      var selected = Categories.Where(_ => _.Selected).Select(_ => _.Category).ToArray();
      return GetFilteredCheckins().VenueVisits().Where(_ => _.Place.Categories.Intersect(selected).Any());
    }
  }
}