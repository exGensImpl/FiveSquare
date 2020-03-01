using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExGens.FiveSquare.UI
{
  internal class ViewModelBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected virtual void OnPropertyChanged<T>(ref T Field, T Value, [CallerMemberName] string propertyName = null)
    {
      if (EqualityComparer<T>.Default.Equals(Field, Value) == false)
      {
        Field = Value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}
