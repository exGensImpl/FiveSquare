using System;
using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class LogVisitCountMetric : IVisitMetric
  {
    private readonly float m_lowerBound;
    private readonly float m_maxVisitTimes;

    public LogVisitCountMetric(IEnumerable<Visit> visits, float lowerBound = 0.3f )
    {
      m_lowerBound = lowerBound;
      m_maxVisitTimes = (float)Math.Log(visits.Max(_ => _.Times));
    }

    public float GetMetric(Visit visit)
      => Math.Min(1.0f, m_lowerBound + (float) Math.Log(visit.Times) / m_maxVisitTimes);
  }
}