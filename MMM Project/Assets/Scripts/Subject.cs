using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    //a collection of all the observers of this subject
    private List<IObserver> _observers = new List<IObserver>();

    //add the observer to the subject's collection
    public void AddObserver(IObserver observer){
        _observers.Add(observer);
    }

    //remove the observer from the subject's collection
    public void RemoveObserver(IObserver observer){
        _observers.Remove(observer);
    }

    //notify each observer that an event has occurred
    protected void NotifyObservers(int index){
        _observers.ForEach((_observers) => {
            _observers.OnNotify(index);
        });
    }

}
