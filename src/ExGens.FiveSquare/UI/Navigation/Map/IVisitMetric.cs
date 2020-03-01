using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal interface IVisitMetric
  {
    float GetMetric(Visit visit);
  }
}