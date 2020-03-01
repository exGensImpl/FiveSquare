using System;
using System.Windows.Controls;
using BruTile;
using BruTile.Predefined;
using BruTile.Web;
using Mapsui;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.UI;
using Mapsui.Utilities;
using Mapsui.Widgets.ScaleBar;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  /// <summary>
  /// Логика взаимодействия для MapView.xaml
  /// </summary>
  public partial class MapView : UserControl
  {
    public MapView()
    {
      InitializeComponent();
      Map.Map = CreateMap(); 
    }

    private static Mapsui.Map CreateMap()
    {
      var map = new Mapsui.Map
      {
        Limiter = new ViewportLimiter
        {
          ZoomLimits = new MinMax(0, 30000)
        },
      };

      map.Home = n => n.ZoomTo(map.Resolutions[12]);
      
      map.Widgets.Add(new Mapsui.Widgets.Zoom.ZoomInOutWidget { Size = 25, MarginX = 20, MarginY = 40 });
      
      return map;
    }
  }
}
