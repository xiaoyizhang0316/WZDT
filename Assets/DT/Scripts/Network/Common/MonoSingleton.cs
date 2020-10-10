using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
{
    /*static T m_instance;

    public static T Instance { get => m_instance; }

    protected virtual void Awake()
    {
        m_instance = this as T;
    }*/

    public static string typeName;
    private static T my = null;
    public static T My
    {
        get
        {
            if (my == null)
            {
                my = FindObjectOfType(typeof(T)) as T;
                if (my == null)
                {
                    my = new GameObject("_" + typeof(T).Name).AddComponent<T>();
                    //DontDestroyOnLoad(instance);
                }
                //if (instance == null)
                //	Console.LogError("Failed to create instance of " + typeof(T).FullName + ".");
                typeName = my.GetType().ToString();
            }
            return my;
        }
    }

    void OnApplicationQuit() { if (my != null) my = null; }

    public static T CreateInstance()
    {
        if (My != null) My.OnCreate();
        return My;
    }

    protected virtual void OnCreate()
    {

    }
}
