using System;
using System.Reactive.Linq;
using ExGens.FiveSquare.Domain;
using ExGens.FiveSquare.Services;
using ReactiveUI;

namespace ExGens.FiveSquare.UI.Navigation
{
  internal abstract class CheckinRangeBasedViewModel : ReactiveObject, IViewModel
  {
    public DateTime FirstCheckin
    {
      get => m_firstCheckin;
      protected set => this.RaiseAndSetIfChanged(ref m_firstCheckin, value);
    }

    public DateTime LastCheckin
    {
      get => m_lastCheckin;
      protected set => this.RaiseAndSetIfChanged(ref m_lastCheckin, value);
    }
    
    public DateTime Start
    {
      get => m_start;
      set => this.RaiseAndSetIfChanged(ref m_start, value);
    }

    public DateTime End
    {
      get => m_end;
      set => this.RaiseAndSetIfChanged(ref m_end, value);
    }

    protected readonly FiveSquareServices m_services;

    private DateTime m_start;
    private DateTime m_end;
    private DateTime m_firstCheckin;
    private DateTime m_lastCheckin;

    protected CheckinRangeBasedViewModel(FiveSquareServices services)
    {
      m_services = services;

      var chekins = services.FiveSquare.GetCheckins();
      var lastCheckinReseived = false;

      chekins.Take(1).Subscribe(_ => End = LastCheckin = _.Date);
      chekins.TakeLast(1).Subscribe(_ =>
      {
        lastCheckinReseived = true;
        Start = FirstCheckin = _.Date;
      });

      chekins.Throttle(TimeSpan.FromSeconds(0.5))
             .TakeWhile(_ => lastCheckinReseived)
             .Subscribe(_ => Start = FirstCheckin = _.Date);
    }

    protected IObservable<Checkin> GetFilteredCheckins()
      => m_services.FiveSquare.GetCheckins().Where(_ => _.Date >= Start && _.Date <= End);
  }
}