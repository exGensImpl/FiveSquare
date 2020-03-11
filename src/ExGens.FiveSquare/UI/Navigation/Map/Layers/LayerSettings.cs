using System;
using System.Collections.Generic;
using System.Linq;
using BruTile.Predefined;
using ExGens.FiveSquare.Domain;
using Mapsui.Styles;

namespace ExGens.FiveSquare.UI.Navigation.Map.Layers
{
  internal delegate IVisitMetric MetricFactory(IReadOnlyCollection<Visits<Venue>> visits, float multiplier);

  internal sealed class LayerSettings : NotifyPropertyChangedTrait
  {
    public KnownTileSource TileSource
    {
      get => m_tileSource;
      set => OnPropertyChanged(ref m_tileSource, value);
    }

    public float CheckinPointOpacity
    {
      get => m_checkinPointOpacity;
      set => OnPropertyChanged(ref m_checkinPointOpacity, value);
    }

    public float CheckinPointMultiplier
    {
      get => m_checkinPointMultiplier;
      set => OnPropertyChanged(ref m_checkinPointMultiplier, value);
    }

    public MetricFactory MetricFactory
    {
      get => m_metricFactory;
      set => OnPropertyChanged(ref m_metricFactory, value);
    }

    public IReadOnlyList<PointScale> Scales
    {
      get => m_scales;
      set => OnPropertyChanged(ref m_scales, value);
    }

    private MetricFactory m_metricFactory;
    private float m_checkinPointMultiplier;
    private float m_checkinPointOpacity;
    private KnownTileSource m_tileSource;
    private IReadOnlyList<PointScale> m_scales;

    public static LayerSettings Default 
      => new LayerSettings
      {
        TileSource = KnownTileSource.OpenStreetMap,
        CheckinPointMultiplier = 3,
        MetricFactory = (v,m) => new LinearVisitCountMetric(v, m),
        CheckinPointOpacity = 0.7f,
        Scales = new []
        {
          new PointScale(  0,     300, 0.35f, 1.2),
          new PointScale( 301,    500, 0.35f, 1.1),
          new PointScale( 501,    700, 0.33f, 1),
          new PointScale( 701,    850, 0.33f, 0.9f),
          new PointScale( 851,   1000, 0.32f, 0.8f),
          new PointScale(1001,   1500, 0.32f, 0.7f),
          new PointScale(1501,   2100, 0.32f, 0.6f),
          new PointScale(2101,   2800, 0.32f, 0.55f),
          new PointScale(2801,   4000, 0.31f, 0.50f),
          new PointScale(4001, 100000, 0.30f, 0.45f),
        }
      };
    
    public IEnumerable<IStyle> GetStyles(Visits<Venue> visit, IVisitMetric metric)
      => Scales.Select(scale 
        => new SymbolStyle
        {
          SymbolScale = scale.Value * (1 + scale.MetricMultiplier * (metric.GetMetric(visit) - 1)),
          Fill = new Brush(Color.Indigo),
          Outline = new Pen(Color.Transparent),
          Opacity = CheckinPointOpacity,
          MinVisible = scale.MinResolution,
          MaxVisible = scale.MaxResolution
        }
      );
  }
}