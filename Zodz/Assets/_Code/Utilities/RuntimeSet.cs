using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class RuntimeSet<T> : ScriptableObject
{
  public List<T> Items = new List<T>();

  public UnityAction onChangeAmount;

  public void Add(T thing)
  {
    if (!Items.Contains(thing)){
      Items.Add(thing);
      onChangeAmount?.Invoke();      
    }
  }

  public void Remove(T thing)
  {
    if (Items.Contains(thing)){
      Items.Remove(thing);
      onChangeAmount?.Invoke();
    }
  }
}
