using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class CacheDatas
{
    public List<CacheData> CacheData = new List<CacheData>();
}

public class CacheData
{

    /// <summary>
    /// 参数
    /// </summary>
    public List<string> Par = new  List<string>();

    /// <summary>
    /// 需要传输的json
    /// </summary>
    public string json;

    
    public string path;
}