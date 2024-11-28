using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    Dictionary<string, object> data = new Dictionary<string, object>();

    public void SetValue(string key, object value)
    {
        data[key] = value;
    }

    public T GetValue<T>(string key)
    {
        if (data.TryGetValue(key, out object value))
        {
            return (T)value;
        }
        return default;
    }
}
