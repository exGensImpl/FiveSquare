using System.Collections.Generic;
using System.Linq;
using BruTile.Predefined;
using ExGens.FiveSquare.Domain;
using Mapsui.Layers;
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

    public ILayer Checkins(IReadOnlyCollection<Visit> visits)
    {
      var metric = visits.Any()
        ? (IVisitMetric)new LogVisitCountMetric(visits, m_settings.CheckinPointMultiplier)
        : new ConstantMetric(1);

      return new MemoryLayer
      {
        Name = "Checkins",
        IsMapInfoLayer = true,
        Style = new SymbolStyle{ Opacity = 0 },
        DataSource = new MemoryProvider(visits.Select(visit => new Feature
        {
          Styles = m_settings.GetStyles(visit, metric).ToArray(),
          Geometry = visit.Location.ToMercator(), 
        }))
      };
    }
  }
}