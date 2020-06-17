using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using IOIntensiveFramework.MonoSingleton;

public class NetworkMgr : MonoSingletonDontDestroy<NetworkMgr>
{
    #region Player datas
    public PlayerDatas playerDatas;
    #endregion

    public void Login(string userName, string password, Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("username", userName);
        keyValues.Add("password", password);

        HttpSend(Url.loginUrl, keyValues, playerDatas, doSuccess, doFail);
    }

    public void UpdateFte(Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        HttpSend(Url.updateFte,keyValues, playerDatas, doSuccess, doFail);
    }

    /// <summary>
    /// 上传数据并接受数据
    /// </summary>
    /// <typeparam name="T">返回数据需要解析成的类型</typeparam>
    /// <param name="url">路径</param>
    /// <param name="keyValues">上传的参数</param>
    /// <param name="t">返回的数据</param>
    /// <param name="doSuccess">达成目的后的操作</param>
    /// <param name="doFail">失败的操作</param>
    /// <param name="failDebug">打印失败信息（debug用）</param>
    private void HttpSend<T>(string url, SortedDictionary<string, string> keyValues, T t, Action doSuccess=null, Action doFail = null)
    {
        HttpManager.My.mask.SetActive(true);
        StartCoroutine(HttpManager.My.HttpSend(url, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                t = JsonUtility.FromJson<T>(response.data);
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
    public string errMsg;
}
