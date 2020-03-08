using LiveCharts.Wpf;

namespace ExGens.FiveSquare.UI.Navigation.Stats
{
  /// <summary>
  /// Логика взаимодействия для ColumnChartView.xaml
  /// </summary>
  public partial class ColumnChartView
  {
    public ColumnChartView()
    {
      InitializeComponent();
      DataContextChanged += ColumnChartView_DataContextChanged;
    }

    private void ColumnChartView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
    {
      if (!(e.NewValue is ColumnChartViewModel dataContext))
      {
        return;
      }

      SeriesCollection.Clear();
      for (int i = 0; i < dataContext.Values; i++)
      {
        SeriesCollection.Add(
          new ColumnSeries
          {
            Title = dataContext[i].Item1,
            Values = dataContext[i].Item2
          });
      }
    }
  }
}
