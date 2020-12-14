using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Threading;

public sealed class JsonHelper
{
    delegate T Func<T>(string json);

    static Dictionary<int, object> caches = new Dictionary<int, object>();

    static Dictionary<int, DateTime> cachesActiveTime = new Dictionary<int, DateTime>();


    private static void refreshActiveTime(int hashCode)
    {
        if (cachesActiveTime.ContainsKey(hashCode))
        {
            cachesActiveTime[hashCode] = DateTime.Now;
        }
        else
        {
            cachesActiveTime.Add(hashCode, DateTime.Now);
        }
    }

    public static string SerializeObject(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T DeserializeObject<T>(string json)
    {

        //var t1 = DateTime.Now;
        var hashCode = json.GetHashCode() + typeof(T).GetHashCode();
        refreshActiveTime(hashCode);
        if (caches.ContainsKey(hashCode))
        {
            try
            {
                return (T)caches[hashCode];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        T t = JsonConvert.DeserializeObject<T>(json);
        caches[hashCode] = t;

        //Debug.LogError("JsonHelper.DeserializeObject:" + (DateTime.Now - t1).TotalMilliseconds + " " + typeof(T) + "  " + Thread.CurrentThread.ManagedThreadId);
        return t;
    }


    public static void DeserializeObjectAsync<T>(string json, Action<T> action)
    {
        Func<T> str = (input) =>
        {
            return JsonConvert.DeserializeObject<T>(input);
        };

        str.BeginInvoke(json, (ar) =>
        {
            T t = str.EndInvoke(ar);

            if (action != null)
            {
                action(t);
            }
        }, null);
    }

}
