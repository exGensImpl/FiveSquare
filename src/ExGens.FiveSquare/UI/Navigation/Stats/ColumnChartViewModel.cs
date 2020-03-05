using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
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
    
    public ColumnChartViewModel(string title, IReadOnlyList<string> labels, IChartValues values)
    {
      Labels = labels;
      Values = new SeriesCollection 
      {
        new ColumnSeries 
        {
          Title = title,
          Values = values
        }
      };
    }

    public static ColumnChartViewModel Create<TSource, T>(
      string title, IEnumerable<TSource> source, int take, Func<TSource, T> valueSelector, Func<TSource, string> labelSelector)
    {
      var top = source.OrderByDescending(valueSelector)
        .Take(take)
        .OrderBy(valueSelector)
        .ToArray();

      return new ColumnChartViewModel(
        title,
        top.Select(labelSelector).ToArray(),
        new ChartValues<T>(top.Select(valueSelector)));
    }
  }
}
