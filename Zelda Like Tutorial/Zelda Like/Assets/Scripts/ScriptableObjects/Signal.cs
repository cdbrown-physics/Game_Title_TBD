using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject
{
    // List of signal listener objects
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        // Going backwards through the list so that if something gets removed then 
        // we don't get an 'out of bounds' error message. 
        for(int i = listeners.Count -1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }

    public void DeregisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }
}
