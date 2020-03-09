using Mapsui.UI;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  /// <summary>
  /// Логика взаимодействия для MapView.xaml
  /// </summary>
  public partial class MapView
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
      
      return map;
    }
  }
}
