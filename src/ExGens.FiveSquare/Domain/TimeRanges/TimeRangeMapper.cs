using System;
using System.Collections.Generic;
using System.Linq;
using ExGens.FiveSquare.Infrastructure;

namespace ExGens.FiveSquare.Domain.TimeRanges
{
  /// <summary>
  /// Maps an <see cref="IHaveDate"/> object to time range which it belongs to
  /// </summary>
  internal sealed class TimeRangeMapper
  {
    public DateTime Start { get; }

    public DateTime End { get; }

    private readonly Func<DateTime, ITimeRange> m_mapper;

    public TimeRangeMapper(
      DateTime start, 
      DateTime end, 
      int monthThreshold = 500, 
      int weekThreshold = 70, 
      int dayThreshold = 4)
    {
      Start = start;
      End = end;
      var days = (End - Start).TotalDays;
      
      if (days > monthThreshold)
      {
        m_mapper = _ => new Month(_);
      }
      else if (days > weekThreshold)
      {
        m_mapper = _ => new Week(_);
      }
      else if (days > dayThreshold)
      {
        m_mapper = _ => new Day(_);
      }
      else
      {
        m_mapper = _ => new Hour(_);
      }
    }
    
    /// <summary>
    /// Returns the time range which the specified <see cref="IHaveDate"/> object belongs to
    /// </summary>
    public ITimeRange GetTimeRange<T>(T entity) where T : IHaveDate => m_mapper(entity.Date);

    public IEnumerable<ITimeRange> GetAllMappedRange()
    {
      var end = m_mapper(End);
      return m_mapper(Start).Unfold(_ => _.GetNext())
                            .TakeWhile(_ => _.CompareTo(end) <= 0);
    }
  }
}