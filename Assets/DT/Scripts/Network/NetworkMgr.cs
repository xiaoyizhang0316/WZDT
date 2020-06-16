using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using IOIntensiveFramework.MonoSingleton;

public class NetworkMgr : MonoSingletonDontDestroy<NetworkMgr>
{
    public GameObject mask;
    public void Login(string userName, string password, Action doSuccess = null, Action doFail = null, string failDebug =null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("username", userName);
        keyValues.Add("password", password);

        HttpSend(keyValues, doSuccess, doFail, failDebug);
    }

    public void UpdateFte(Action doSuccess = null, Action doFail = null, string failDebug = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        HttpSend(keyValues, doSuccess, doFail, failDebug);
    }

    private void HttpSend(SortedDictionary<string, string> keyValues, Action doSuccess=null, Action doFail = null, string failDebug=null)
    {
        HttpManager.My.mask.SetActive(true);
        StartCoroutine(HttpManager.My.HttpSend(Url.loginUrl, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                if(failDebug!=null)
                    Debug.LogWarning(failDebug + response.data);
                doFail?.Invoke();
            }
            else
            {
                
                
                doSuccess?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, keyValues.Count==0?HttpType.Get:HttpType.Post));
    }
}

[Serializable]
public class HttpResponse
{
    public int status;
    public string data;
}
