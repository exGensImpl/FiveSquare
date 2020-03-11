using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.Map.Layers
{
  internal interface IVisitMetric
  {
    float GetMetric(Visits<Venue> visit);
  }
}