using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal sealed class ColumnChartViewModel : NotifyPropertyChangedTrait
  {
    public SeriesCollection Values
    {
      get => m_values;
      set => OnPropertyChanged(ref m_values, value);
    }

    public IReadOnlyList<string> Labels
    {
      get => m_labels;
      set => OnPropertyChanged(ref m_labels, value);
    }

    private IReadOnlyList<string> m_labels;
    private SeriesCollection m_values;
    
    public ColumnChartViewModel(
      IReadOnlyList<string> labels, 
      ISeriesView values, 
      ISeriesView values2)
    {
      Labels = labels;
      Values = new SeriesCollection { values, values2 };
    }

    public static ColumnChartViewModel Create<TSource, TValue1, TValue2>(
      IEnumerable<TSource> source, 
      int take, 
      Func<TSource, string> labelSelector,
      string primaryValueTitle, 
      Func<TSource, TValue1> primaryValueSelector, 
      string secondaryValueTitle, 
      Func<TSource, TValue2> secondaryValueSelector)
    {
      var primaryValues = source.OrderByDescending(primaryValueSelector)
        .Take(take)
        .OrderBy(primaryValueSelector)
        .ThenByDescending(labelSelector)
        .ToArray();

      return new ColumnChartViewModel(
        primaryValues.Select(labelSelector).ToArray(),
        new ColumnSeries
        {
          Title = primaryValueTitle,
          Values = new ChartValues<TValue1>(primaryValues.Select(primaryValueSelector))
        }, 
        new ColumnSeries
        {
          Title = secondaryValueTitle,
          Values = new ChartValues<TValue2>(primaryValues.Select(secondaryValueSelector))
        });
    }
  }
}
