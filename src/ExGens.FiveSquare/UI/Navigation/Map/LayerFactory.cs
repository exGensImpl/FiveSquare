using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Utilities;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal static class LayerFactory
  {
    public static ILayer Map() => OpenStreetMap.CreateTileLayer();

    public static ILayer Checkins(IEnumerable<Visit> visits, IVisitMetric metric = null)
    {
      metric = metric ?? new LogVisitCountMetric(visits, 0.25f);

      return new MemoryLayer
      {
        Name = "Points",
        IsMapInfoLayer=true,
        DataSource = new MemoryProvider(visits.Select(ToFeature)),
        Style = new SymbolStyle{ Opacity = 0 }
      };

      IFeature ToFeature(Visit visit)
        => new Feature
        {
          Geometry = SphericalMercator.FromLonLat(
            visit.Location.Longitude, 
            visit.Location.Latitude), 

          Styles = new []
          {
            new SymbolStyle
            {
              SymbolScale = 1.2f * metric.GetMetric(visit),
              Fill = new Brush(Color.Indigo), 
              Outline = new Pen(Color.Transparent),
              Opacity = 0.7f
            }
          }
        };
    }
  }
}