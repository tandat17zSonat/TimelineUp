using System.Collections.Generic;
using UnityEngine;

public static class SOManager
{
    public static Dictionary<object, object> dictionary = new();

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
}
