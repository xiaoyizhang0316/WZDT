using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MyPrefs
{
    public static void SetValue<T>(string key, T value)
    {
        var type = value.GetType();
        if (type == typeof(string)|| (type == typeof(Int64)))
        {
            PlayerPrefs.SetString(key, value.ToString());
        }
        else if (type == typeof(float))
        {
            PlayerPrefs.SetFloat(key, Convert.ToSingle(value));
        }
        else if (type == typeof(int))
        {
            PlayerPrefs.SetInt(key, Convert.ToInt32(value));
        }
        else if (type == typeof(bool))
        {
            PlayerPrefs.SetInt(key, Convert.ToInt32(value) > 0 ? 1 : 0);
        }
        else if (type is object)
        { 
            PlayerPrefs.SetString(key, JsonHelper.SerializeObject(value));
        }
        else
        {
            Debug.LogError("key:" + key + " type:" + value.GetType());
        }
    }

    public static T GetValue<T>(string key)// where T : IConvertible
    {
        var type = typeof(T);

        if (type == typeof(string) || (type == typeof(Int64)))
        {
            return (T)Convert.ChangeType(PlayerPrefs.GetString(key, string.Empty), type);
        }
        else if (type == typeof(float))
        {
            return (T)Convert.ChangeType(PlayerPrefs.GetFloat(key, 0.0f), type);
        }
        else if (type == typeof(int) || type==typeof(Enum))
        {
            return (T)Convert.ChangeType(PlayerPrefs.GetInt(key), type);
        }
        else if (type == typeof(bool))
        {
            return (T)Convert.ChangeType(PlayerPrefs.GetInt(key) > 0, type);
        }
        else if (type is object)
        {
            var str = PlayerPrefs.GetString(key,string.Empty);
            if (string.IsNullOrEmpty(str))
            {
                return default(T);
            }
            else
                return JsonHelper.DeserializeObject<T>(str);
        }
        return default(T);
    }
    
    public static T GetValue<T>(string key, T defaultValue) //where T : IConvertible
    {
        if (!PlayerPrefs.HasKey(key))
        {
            return defaultValue;
        }
        return GetValue<T>(key);
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}
