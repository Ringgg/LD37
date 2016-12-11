using System.Collections.Generic;
using System;

public enum EventType
{
    BossDied,
    HeroesKilled,
    BossAtHalfHp,
    EndAoePhase,
    EndHealPhase,
    EndZonePhase,
    EndGasPhase,
    StartAoePhase,
    StartHealPhase,
    StartZonePhase,
    StartGasPhase,
    StartDefaultPhase,
}

class ObjectEvent
{
    List<Action<object>> objectListeners = new List<Action<object>>();
    List<Action> listeners = new List<Action>();

    public void AddListener(Action<object> e) { objectListeners.Add(e); }
    public void AddListener(Action e) { listeners.Add(e); }

    public void RemoveListener(Action<object> e) { objectListeners.Remove(e); }
    public void RemoveListener(Action e) { listeners.Remove(e); }

    public void Invoke(object o = null)
    {
        for (int i = objectListeners.Count - 1; i >= 0; --i) objectListeners[i](o);
        for (int i = listeners.Count - 1; i >= 0; --i) listeners[i]();
    }
}

public class EventManager
{
    private static EventManager eventManager;
    public static EventManager instance
    {
        get
        {
            if (eventManager == null)
            {
                eventManager = new EventManager();
                eventManager.eventDictionary = new Dictionary<EventType, ObjectEvent>();
            }
            return eventManager;
        }
    }

    private Dictionary<EventType, ObjectEvent> eventDictionary;
    private ObjectEvent tmp;

    public static void StartListening(EventType eventName, Action<object> listener)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out instance.tmp))
        {
            instance.tmp.AddListener(listener);
        }
        else
        {
            instance.tmp = new ObjectEvent();
            instance.tmp.AddListener(listener);
            instance.eventDictionary.Add(eventName, instance.tmp);
        }
    }

    public static void StartListening(EventType eventName, Action listener)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out instance.tmp))
        {
            instance.tmp.AddListener(listener);
        }
        else
        {
            instance.tmp = new ObjectEvent();
            instance.tmp.AddListener(listener);
            instance.eventDictionary.Add(eventName, instance.tmp);
        }
    }

    public static void StopListening(EventType eventName, Action<object> listener)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out instance.tmp))
            instance.tmp.RemoveListener(listener);
    }

    public static void StopListening(EventType eventName, Action listener)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out instance.tmp))
            instance.tmp.RemoveListener(listener);
    }

    public static void TriggerEvent(EventType eventName, object sender = null)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out instance.tmp))
            instance.tmp.Invoke(sender);
    }
}