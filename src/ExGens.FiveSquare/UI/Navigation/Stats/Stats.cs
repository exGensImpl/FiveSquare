using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Domain.TimeRanges;
using ExGens.FiveSquare.Infrastructure;
using ExGens.FiveSquare.Properties;
using LiveCharts;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal readonly struct Stats
  {
    public IReadOnlyList<ITimeRange> TimeRanges { get; }

    public IChartValues CheckinsByTimeRange { get; }

    public ColumnChartViewModel Categories { get; }
  
    public static Stats Calculate(IReadOnlyList<Checkin> checkins, TimeRangeMapper timeMapper)
    {
      var timeRanges = timeMapper.GetAllMappedRange().ToArray();

      var groupedCheckins = checkins
        .GroupBy(timeMapper.GetTimeRange)
        .OrderBy(_ => _.Key)
        .ToDictionary(_ => _.Key, _ => _.Count());

      var checkinsByRange = new ChartValues<int>(
        timeRanges.Select(_ => groupedCheckins.GetOrElse(_, 0)).ToArray());

      var categoriesChart = ColumnChartViewModel.Create(
        CategoryStats.Of(checkins.VenueVisits()), 20, _ => _.Category.Name,
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