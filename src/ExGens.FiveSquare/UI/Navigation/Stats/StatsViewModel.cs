using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using ExGens.FiveSquare.Services;
using LiveCharts;
using ReactiveUI;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal sealed class StatsViewModel : ReactiveObject, IViewModel
  {
    public DateTime FirstCheckin
    {
      get => m_firstCheckin;
      private set => this.RaiseAndSetIfChanged(ref m_firstCheckin, value);
    }

    public DateTime LastCheckin
    {
      get => m_lastCheckin;
      private set => this.RaiseAndSetIfChanged(ref m_lastCheckin, value);
    }

    public DateTime Start
    {
      get => m_start;
      set => this.RaiseAndSetIfChanged(ref m_start, value);
    }

    public DateTime End
    {
      get => m_end;
      set => this.RaiseAndSetIfChanged(ref m_end, value);
    }

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
    private DateTime m_start;
    private DateTime m_end;
    private DateTime m_firstCheckin;
    private DateTime m_lastCheckin;

    public StatsViewModel(FiveSquareServices services)
    {
      this.WhenAnyValue(_ => _.Start, _ => _.End)
          .Throttle(TimeSpan.FromSeconds(0.5), RxApp.TaskpoolScheduler)
          .Select(_ => Stats.Calculate(services.FiveSquare.GetCheckins(), _.Item1, _.Item2))
          .ObserveOn(RxApp.MainThreadScheduler)
          .Subscribe(stats =>
          {
            TimeRangeLabels = stats.TimeRanges.Select(_ => _.ShortDescription).ToArray();
            CheckinsByTimeRange = stats.CheckinsByTimeRange;
            Categories = stats.Categories;
          });

      var chekins = services.FiveSquare.GetCheckins();
      var lastCheckinReseived = false;

      chekins.Take(1).Subscribe(_ => End = LastCheckin = _.Date);
      chekins.TakeLast(1).Subscribe(_ =>
      {
        lastCheckinReseived = true;
        Start = FirstCheckin = _.Date;
      });

      chekins.Throttle(TimeSpan.FromSeconds(0.5))
             .TakeWhile(_ => lastCheckinReseived)
             .Subscribe(_ => Start = FirstCheckin = _.Date);
    }
  }
}
