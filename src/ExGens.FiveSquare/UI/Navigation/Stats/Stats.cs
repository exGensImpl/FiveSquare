using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Properties;
using LiveCharts;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal readonly struct Stats
  {
    public IReadOnlyList<string> WeekLabels { get; }

    public IChartValues CheckinsByWeek { get; }

    public ColumnChartViewModel Categories { get; }

    public static Stats Calculate(IObservable<Checkin> checkins, DateTime start, DateTime end)
      => Calculate(checkins.Where(_ => _.Date <= end && _.Date >= start));
      
    public static Stats Calculate(IObservable<Checkin> checkins)
    {
      var chekinsByWeek = checkins.ToEnumerable()
        .GroupBy(_ => new Week(_.Date))
        .OrderBy(_ => _.Key)
        .ToArray();

      var weekLabels = chekinsByWeek.Select(_ => _.Key.Months).ToArray();
      var checkinsByWeek = new ChartValues<int>(chekinsByWeek.Select(_ => _.Count()).ToArray());

      var categoriesChart = ColumnChartViewModel.Create(
        CategoryStats.FromCheckins(checkins.ToEnumerable()), 20, _ => _.Category.Name,
        Resources.StatsView_Visits, _ => _.Visits,
        Resources.StatsView_Places, _ => _.Places);

      return new Stats(weekLabels, checkinsByWeek, categoriesChart);
    }

    public Stats(IReadOnlyList<string> weekLabels, IChartValues checkinsByWeek, ColumnChartViewModel categories)
    {
      WeekLabels = weekLabels;
      CheckinsByWeek = checkinsByWeek;
      Categories = categories;
    }
  }
}