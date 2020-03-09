using System;
using System.Windows;
using System.Windows.Controls;

namespace ExGens.FiveSquare.UI.Navigation
{
  /// <summary>
  /// Логика взаимодействия для BoundarySlider.xaml
  /// </summary>
  public partial class RangeSlider : UserControl
  {
    public DateTime Minimum
    {
      get => (DateTime)GetValue(MinimumProperty);
      set => SetValue(MinimumProperty, value);
    }

    public static readonly DependencyProperty MinimumProperty =
      DependencyProperty.Register(
        nameof(Minimum), 
        typeof(DateTime), 
        typeof(RangeSlider), 
        new UIPropertyMetadata(new DateTime(0)));

    public DateTime LowerValue
    {
      get => (DateTime)GetValue(LowerValueProperty);
      set => SetValue(LowerValueProperty, value);
    }

    public static readonly DependencyProperty LowerValueProperty =
      DependencyProperty.Register(
        nameof(LowerValue), 
        typeof(DateTime), 
        typeof(RangeSlider), 
        new UIPropertyMetadata(new DateTime(0)));

    public DateTime UpperValue
    {
      get => (DateTime)GetValue(UpperValueProperty);
      set => SetValue(UpperValueProperty, value);
    }

    public static readonly DependencyProperty UpperValueProperty =
      DependencyProperty.Register(
        nameof(UpperValue), 
        typeof(DateTime), 
        typeof(RangeSlider), 
        new UIPropertyMetadata(DateTime.Now));

    public DateTime Maximum
    {
      get => (DateTime)GetValue(MaximumProperty);
      set => SetValue(MaximumProperty, value);
    }

    public static readonly DependencyProperty MaximumProperty =
      DependencyProperty.Register(
        nameof(Maximum), 
        typeof(DateTime), 
        typeof(RangeSlider), 
        new UIPropertyMetadata(DateTime.Now));

    public RangeSlider()  
    {
      InitializeComponent();
      this.Loaded += RangeSlider_Loaded;
    }

    void RangeSlider_Loaded(object sender, RoutedEventArgs e)
    {
      LowerSlider.ValueChanged += LowerSlider_ValueChanged;
      UpperSlider.ValueChanged += UpperSlider_ValueChanged;
    }

    private void LowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      UpperSlider.Value = Math.Max(UpperSlider.Value, LowerSlider.Value);
    }

    private void UpperSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      LowerSlider.Value = Math.Min(UpperSlider.Value, LowerSlider.Value);
    }
  }
}
