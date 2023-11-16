using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISubject : MonoBehaviour
{
    [SerializeField] List<IObserver> observers = new List<IObserver>();
 
    public void AddObserver(IObserver observer_)
    {
        observers.Add(observer_);
    }
    public void RemoveObserver(IObserver observer_)
    {
        observers.Remove(observer_);
    }
    public void NotifyObserver(PlayerState action_)
    {
        foreach(IObserver n in observers)
        {
            n.OnNotify(action_);
        }
    }
}
