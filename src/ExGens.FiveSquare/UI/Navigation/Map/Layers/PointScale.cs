namespace ExGens.FiveSquare.UI.Navigation.Map.Layers
{
  internal readonly struct PointScale
  {
    public double MinResolution { get; }

    public double MaxResolution { get; }

    public double Value { get; }

    public double MetricMultiplier { get; }

    public PointScale(double minResolution, double maxResolution, double value, double metricMultiplier)
    {
      MinResolution = minResolution;
      MaxResolution = maxResolution;
      Value = value;
      MetricMultiplier = metricMultiplier;
    }
  }
}