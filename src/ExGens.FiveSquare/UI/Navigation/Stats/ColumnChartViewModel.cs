using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  internal sealed class ColumnChartViewModel : NotifyPropertyChangedTrait
  {
    public Tuple<string, IChartValues> this[int index] => m_values[index];

    public int Values => m_values.Length;

    public IReadOnlyList<string> Labels
    {
      get => m_labels;
      set => OnPropertyChanged(ref m_labels, value);
    }

    private IReadOnlyList<string> m_labels;
    private readonly Tuple<string, IChartValues>[] m_values;

    private ColumnChartViewModel(
      IReadOnlyList<string> labels,
      params Tuple<string, IChartValues>[] values)
    {
      Labels = labels;
      m_values = values;
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
        Tuple.Create(
          primaryValueTitle, 
          (IChartValues)new ChartValues<TValue1>(primaryValues.Select(primaryValueSelector))),
        Tuple.Create(
          secondaryValueTitle, 
          (IChartValues)new ChartValues<TValue2>(primaryValues.Select(secondaryValueSelector))));
    }
  }
}
