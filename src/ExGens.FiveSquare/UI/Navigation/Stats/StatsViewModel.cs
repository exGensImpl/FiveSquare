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
    public DateTime FirstCheckin { get; }

    public DateTime LastCheckin { get; }

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

    public IChartValues CheckinsByWeek
    {
      get => m_checkinsByWeek;
      private set => this.RaiseAndSetIfChanged(ref m_checkinsByWeek, value);
    }

    public IReadOnlyList<string> WeekLabels
    {
      get => m_weekLabels;
      private set => this.RaiseAndSetIfChanged(ref m_weekLabels, value);
    }

    public ColumnChartViewModel Categories
    {
      get => m_categories;
      private set => this.RaiseAndSetIfChanged(ref m_categories, value);
    }

    private readonly FiveSquareServices m_services;
    private ColumnChartViewModel m_categories;
    private IReadOnlyList<string> m_weekLabels;
    private IChartValues m_checkinsByWeek;
    private DateTime m_start;
    private DateTime m_end;

    public StatsViewModel(FiveSquareServices services)
    {
      m_services = services;

      this.WhenAnyValue(_ => _.Start, _ => _.End)
          .Throttle(TimeSpan.FromMilliseconds(250))
          .ObserveOn(RxApp.TaskpoolScheduler)
          .Select(_ => Stats.Calculate(m_services.FiveSquare.GetCheckins(), _.Item1, _.Item2))
          .ObserveOn(RxApp.MainThreadScheduler)
          .Subscribe(stats =>
          {
            WeekLabels = stats.WeekLabels;
            CheckinsByWeek = stats.CheckinsByWeek;
            Categories = stats.Categories;
          });
      
      var chekins = services.FiveSquare.GetCheckins().ToEnumerable().ToArray();

      if (chekins.Any())
      {
        Start = FirstCheckin = chekins.Last().Date;
        End = LastCheckin = chekins.First().Date;
      }
    }
  }
}
