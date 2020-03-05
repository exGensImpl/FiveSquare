using System;
using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Properties;
using ExGens.FiveSquare.Services;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal sealed class StatsViewModel : ViewModelBase
  {
    public ColumnChartViewModel CategoryByVisits
    {
      get => m_categoryByVisits;
      set => OnPropertyChanged(ref m_categoryByVisits, value);
    }

    public ColumnChartViewModel CategoryByPlaces
    {
      get => m_categoryByPlaces;
      set => OnPropertyChanged(ref m_categoryByPlaces, value);
    }

    private readonly FiveSquareServices m_services;
    private ColumnChartViewModel m_categoryByVisits;
    private ColumnChartViewModel m_categoryByPlaces;

    public StatsViewModel(FiveSquareServices services)
    {
      m_services = services;

      var categories =
        CategoryStats.FromVisits(services.FiveSquare.GetVisits()).ToArray();

      CategoryByVisits = ColumnChartViewModel.Create(
        Resources.StatsView_Visits, categories, 15, _ => _.Visits, _ => _.Category.Name);

      CategoryByPlaces = ColumnChartViewModel.Create(
        Resources.StatsView_Places, categories, 15, _ => _.Places, _ => _.Category.Name);
    }
  }
}
