using System.Collections.Generic;
using System.Linq;
using BruTile.Predefined;
using ExGens.FiveSquare.Domain;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class LayerFactory
  {
    private readonly LayerSettings m_settings;

    public LayerFactory(LayerSettings settings)
    {
      m_settings = settings;
    }

    public ILayer Map() 
      => new TileLayer(KnownTileSources.Create(m_settings.TileSource));

    public ILayer Checkins(IReadOnlyCollection<Visit> visits, IVisitMetric metric = null)
    {
      metric = metric ?? new LogVisitCountMetric(visits, 3);

      return new MemoryLayer
      {
        Name = "Checkins",
        IsMapInfoLayer = true,
        DataSource = new MemoryProvider(visits.Select(ToFeature)),
        Style = new SymbolStyle{ Opacity = 0 }
      };

      IFeature ToFeature(Visit visit)
        => new Feature
        {
          Styles = GetStyles(visit).ToArray(),
          Geometry = SphericalMercator.FromLonLat(
            visit.Location.Longitude, 
            visit.Location.Latitude), 
        };

      IEnumerable<IStyle> GetStyles(Visit visit)
        => m_settings.Scales.Select(scale 
          => new SymbolStyle
          {
            SymbolScale = scale.Value * (1 + scale.MetricMultiplier * (metric.GetMetric(visit) - 1)),
            Fill = new Brush(Color.Indigo),
            Outline = new Pen(Color.Transparent),
            Opacity = 0.7f,
            MinVisible = scale.MinResolution,
            MaxVisible = scale.MaxResolution
          }
        );
    }
  }
}