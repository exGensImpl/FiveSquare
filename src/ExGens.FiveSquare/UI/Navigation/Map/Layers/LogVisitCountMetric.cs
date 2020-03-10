using System;
using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.Map.Layers
{
  internal sealed class LogVisitCountMetric : IVisitMetric
  {
    private readonly float m_maxMultiplier;
    private readonly float m_maxVisitTimes;

    public LogVisitCountMetric(IEnumerable<Visit> visits, float maxMultiplier = 0.3f)
    {
      m_maxMultiplier = maxMultiplier;
      m_maxVisitTimes = (float) Math.Log(visits.Max(_ => _.Times) + 1);
    }

    public float GetMetric(Visit visit)
      => 1 + Math.Min(m_maxMultiplier - 1, m_maxMultiplier * (float) Math.Log(visit.Times) / m_maxVisitTimes);
  }
}