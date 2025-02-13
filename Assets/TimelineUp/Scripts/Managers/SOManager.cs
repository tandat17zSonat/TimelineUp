using System.Collections.Generic;
using UnityEngine;

public static class SOManager
{
    public static Dictionary<object, object> dictionary = new();
    public static Dictionary<int, object> dictionaryTimelineSO = new();

    public static T GetSO<T>() where T : ScriptableObject
    {
        if (dictionary.ContainsKey(typeof(T)))
        {
            return (T)dictionary[typeof(T)];
        }
        else
        {
            Debug.Log($"Load {typeof(T)} from resources");

            object obj = Resources.Load<T>($"SO/{typeof(T)}");
            dictionary[typeof(T)] = obj;
            return (T)obj;
        }
    }

    public static T GetSO<T>(int timelineId, int eraId) where T : ScriptableObject
    {
        int key = timelineId * 10 + eraId;
        if (dictionaryTimelineSO.ContainsKey(key))
        {
            return (T)dictionary[key];
        }
        else
        {
            Debug.Log($"Load {typeof(T)} from resources {timelineId} {eraId}");

            object obj = Resources.Load<T>($"SO/Timeline/{timelineId}_{eraId}");
            dictionary[key] = obj;
            return (T)obj;
        }
    }
}
