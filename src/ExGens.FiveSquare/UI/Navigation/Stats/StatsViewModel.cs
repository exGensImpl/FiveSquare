using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Properties;
using ExGens.FiveSquare.Services;
using LiveCharts;
using LiveCharts.Wpf;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal sealed class StatsViewModel : ViewModelBase
  {
    public ColumnChartViewModel Categories
    {
      get => m_categories;
      set => OnPropertyChanged(ref m_categories, value);
    }

    public SeriesCollection CheckinsByWeek { get; }

    public IReadOnlyList<string> WeekLabels { get; }

    private readonly FiveSquareServices m_services;
    private ColumnChartViewModel m_categories;

    public StatsViewModel(FiveSquareServices services)
    {
      m_services = services;

      var categories =
        CategoryStats.FromVisits(services.FiveSquare.GetVisits()).ToArray();

      Categories = ColumnChartViewModel.Create(
        categories, 20, _ => _.Category.Name,
        Resources.StatsView_Visits, _ => _.Visits,
        Resources.StatsView_Places, _ => _.Places);

      var chekinsByWeek = services
        .FiveSquare
        .GetCheckins()
        .GroupBy(_ => new Week(_.Date))
        .OrderBy( _ => _.Key )
        .ToArray();

      WeekLabels = chekinsByWeek.Select(_ => _.Key.Months).ToArray();
      CheckinsByWeek = new SeriesCollection
      {
        new LineSeries
        {
          Title = Resources.StatsView_Checkins,
          Values = new ChartValues<int>(chekinsByWeek.Select(_ => _.Count()).ToArray())
        }
      };
    }
  }
}
