using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimEvents : MonoBehaviour
{
    /// <summary>
    /// Esta clase es un diccionario de strings con acciones para que se llamen dentro de una animacion y se declaren que funcion debe tener esa action
    /// en el codigo
    /// </summary>
    Dictionary<string, Action> events = new Dictionary<string, Action>();

    public void ADD_EVENT(string eventname, Action callback)
    {
        if (!events.ContainsKey(eventname))
        {
            events.Add(eventname, callback);
        }
        else
        {
            throw new Exception("null animation!");
        }
    }

    public void ANIM_EVENT(string parameters)
    {
        events[parameters].Invoke();
    }
    
}
