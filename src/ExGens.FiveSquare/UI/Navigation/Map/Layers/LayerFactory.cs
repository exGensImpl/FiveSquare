using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using BruTile.Predefined;
using ExGens.FiveSquare.Domain;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Styles;

namespace ExGens.FiveSquare.UI.Navigation.Map.Layers
{
  internal sealed class LayerFactory
  {
    private const string CheckinLayer = "Checkins";

    public IReadOnlyList<ILayer> Layers { get; }

    private readonly LayerSettings m_settings;

    public LayerFactory(LayerSettings settings)
    {
      m_settings = settings;
      Layers = new [] { Map(), Checkins() };
    }

    public void UpdateCheckins(IObservable<Visits<Venue>> source)
    {
      var layer = Layers.OfType<MemoryLayer>() .FirstOrDefault(_ => _.Name == CheckinLayer);

      if (!(layer?.DataSource is MemoryProvider chekinProvider))
      {
        return;
      }

      var checkinList = new List<Visits<Venue>>();

      chekinProvider.Clear();

      source.Buffer(200).Subscribe(checkins =>
      {
        checkinList.AddRange(checkins);
        chekinProvider.ReplaceFeatures(ToFeatures(checkinList));
        layer.DataHasChanged();
      });

      layer.DataHasChanged();
    }

    private ILayer Map() 
      => new TileLayer(KnownTileSources.Create(m_settings.TileSource));

    private ILayer Checkins(IReadOnlyCollection<Visits<Venue>> checkins = null)
      => new MemoryLayer
        {
          Name = CheckinLayer,
          IsMapInfoLayer = true,
          Style = new SymbolStyle{ Opacity = 0 },
          DataSource = new MemoryProvider(ToFeatures(checkins ?? Array.Empty<Visits<Venue>>()))
        };

    private IEnumerable<IFeature> ToFeatures(IReadOnlyCollection<Visits<Venue>> checkins)
    {
      var metric = GetMetric(checkins);
      return checkins.Select(_ => _.ToFeature(m_settings.GetStyles(_, metric).ToArray()));
    }

    private IVisitMetric GetMetric(IReadOnlyCollection<Visits<Venue>> visits)
      => visits.Any()
      ? m_settings.MetricFactory(visits, m_settings.CheckinPointMultiplier)
      : new ConstantMetric(1);

  }
}