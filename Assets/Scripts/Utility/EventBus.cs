using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static Dictionary<Type, Delegate> Events = new();
    public static void Subscript<T>(Action<T> callback)
    {
        Type type = typeof(T);
        if (!Events.ContainsKey(type))
        {
            Events[type] = null;
        }
        Events[type] = (Action<T>)Events[type] + callback;
    }
    public static void Unsubscript<T>(Action<T> callback)
    {
        Type type = typeof(T);
        if (!Events.ContainsKey(type)) { return; }
        Events[type] = (Action<T>)Events[type] - callback;
        if (Events[type] == null)
        {
            Events.Remove(type);
        }
    }
    public static void Publish<T>(T Event)
    {
        Type type = typeof(T);
        if (Events.TryGetValue(type, out Delegate @delegate))
        {
            Action<T> callback = @delegate as Action<T>;
            callback?.Invoke(Event);
        }
        Debug.Log($"Publish Event:{Event.GetType()}");
    }
    public static void Clear()
    {
        Events.Clear();
    }
}