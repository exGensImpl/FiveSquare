using System;
using System.Collections.Generic;
using System.ComponentModel;
using ExGens.FiveSquare.Infrastructure;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal static class BindingListExtensions
  {
    public static void BatchForeach<T>(this BindingList<T> list, Action<T> action)
    {
      list.RaiseListChangedEvents = false;
      list.Foreach(action);
      list.RaiseListChangedEvents = true;
      list.ResetBindings();
    }

    public static void Replace<T>(this BindingList<T> list, IEnumerable<T> elems)
    {
      list.RaiseListChangedEvents = false;
      list.Clear();
      elems.Foreach(list.Add);
      list.RaiseListChangedEvents = true;
      list.ResetBindings();
    }
  }
}