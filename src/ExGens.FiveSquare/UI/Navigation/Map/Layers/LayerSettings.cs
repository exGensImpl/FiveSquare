using System.Collections.Generic;
using System.Linq;
using BruTile.Predefined;
using ExGens.FiveSquare.Domain;
using Mapsui.Styles;

namespace ExGens.FiveSquare.UI.Navigation.Map.Layers
{
  internal sealed class LayerSettings
  {
    public KnownTileSource TileSource { get; set; }

    public float CheckinPointOpacity { get; set; }

    public float CheckinPointMultiplier { get; set; }

    public IReadOnlyList<PointScale> Scales { get; set; }

    public static LayerSettings Default 
      => new LayerSettings
      {
        TileSource = KnownTileSource.OpenStreetMap,
        CheckinPointMultiplier = 3,
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
    
    public IEnumerable<IStyle> GetStyles(Visit visit, IVisitMetric metric)
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