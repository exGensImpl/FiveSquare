using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.Map.Layers
{
  internal sealed class LinearVisitCountMetric : IVisitMetric
  {
    private readonly float m_maxMultiplier;
    private readonly float m_maxVisitTimes;

    public LinearVisitCountMetric(IEnumerable<Visits<Venue>> visits, float maxMultiplier = 0.3f )
    {
      m_maxMultiplier = maxMultiplier;
      m_maxVisitTimes = visits.Max(_ => _.Times) + 1;
    }

    public float GetMetric(Visits<Venue> visit)
      => 1 + visit.Times / m_maxVisitTimes * (m_maxMultiplier - 1);
  }
}