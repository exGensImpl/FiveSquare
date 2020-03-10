using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using ExGens.FiveSquare.Domain.TimeRanges;
using ExGens.FiveSquare.Services;
using LiveCharts;
using ReactiveUI;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal sealed class StatsViewModel : CheckinRangeBasedViewModel
  {
    public IChartValues CheckinsByTimeRange
    {
      get => m_checkinsByTimeRange;
      private set => this.RaiseAndSetIfChanged(ref m_checkinsByTimeRange, value);
    }

    public IReadOnlyList<string> TimeRangeLabels
    {
      get => m_timeRangeLabels;
      private set => this.RaiseAndSetIfChanged(ref m_timeRangeLabels, value);
    }

    public ColumnChartViewModel Categories
    {
      get => m_categories;
      private set => this.RaiseAndSetIfChanged(ref m_categories, value);
    }

    private ColumnChartViewModel m_categories;
    private IReadOnlyList<string> m_timeRangeLabels;
    private IChartValues m_checkinsByTimeRange;

    public StatsViewModel(FiveSquareServices services) : base(services)
    {
      this.WhenAnyValue(_ => _.Start, _ => _.End)
          .Throttle(TimeSpan.FromSeconds(0.5), RxApp.TaskpoolScheduler)
          .Select(_ => Stats.Calculate(
              GetFilteredCheckins().ToEnumerable().ToArray(), 
              new TimeRangeMapper(_.Item1, _.Item2)))
          .ObserveOn(RxApp.MainThreadScheduler)
          .Subscribe(stats =>
          {
            TimeRangeLabels = stats.TimeRanges.Select(_ => _.ShortDescription).ToArray();
            CheckinsByTimeRange = stats.CheckinsByTimeRange;
            Categories = stats.Categories;
          });
    }
  }
}
