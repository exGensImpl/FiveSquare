using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Domain.TimeRanges;
using ExGens.FiveSquare.Properties;
using LiveCharts;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal readonly struct Stats
  {
    public IReadOnlyList<ITimeRange> TimeRanges { get; }

    public IChartValues CheckinsByTimeRange { get; }

    public ColumnChartViewModel Categories { get; }

    public static Stats Calculate(IObservable<Checkin> checkins, DateTime start, DateTime end)
      => Calculate(checkins.Where(_ => _.Date <= end && _.Date >= start), new TimeRangeMapper(start, end));
      
    public static Stats Calculate(IObservable<Checkin> checkins, TimeRangeMapper timeMapper)
    {
      var allRanges = timeMapper.GetAllMappedRange().ToArray();

      var groupedCheckins = checkins.ToEnumerable()
        .GroupBy(timeMapper.GetTimeRange)
        .OrderBy(_ => _.Key)
        .ToDictionary(_ => _.Key, _ => _.Count());

      var timeRanges = allRanges.ToArray();
      var checkinsByRange = new ChartValues<int>(
        allRanges.Select(_ => groupedCheckins.ContainsKey(_) ? groupedCheckins[_] : 0).ToArray());

      var categoriesChart = ColumnChartViewModel.Create(
        CategoryStats.FromCheckins(checkins.ToEnumerable()), 20, _ => _.Category.Name,
        Resources.StatsView_Visits, _ => _.Visits,
        Resources.StatsView_Places, _ => _.Places);

      return new Stats(timeRanges, checkinsByRange, categoriesChart);
    }

    private Stats(IReadOnlyList<ITimeRange> timeRanges, IChartValues checkinsByTimeRange, ColumnChartViewModel categories)
    {
      TimeRanges = timeRanges;
      CheckinsByTimeRange = checkinsByTimeRange;
      Categories = categories;
    }
  }
}