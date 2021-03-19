using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheSystem : MonoSingleton<CacheSystem>
{
    public List<string> cacheList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Save(CacheData cacheData)
    {
        //PrefsManager.
        if (!PrefsManager.inst.HasKey("cache"))
        {
            List<CacheData> cachelist1 = new List<CacheData>();
            CacheDatas datas1 = new CacheDatas();
            cachelist1.Add(cacheData);
            datas1.CacheData = cachelist1;
            PrefsManager.SetValue("cache", datas1);
            //Debug.Log("创建成功");
        }
        else
        {
            List<CacheData> cachelist = PrefsManager.GetValue<CacheDatas>("cache").CacheData;
            if (!cachelist.Contains(cacheData))
            {
                cachelist.Add(cacheData);
            }

            CacheDatas datas = new CacheDatas();
            datas.CacheData = cachelist;
            PrefsManager.SetValue("cache", datas);
            //Debug.Log("储存成功");

        }
    }

    public List<CacheData> Get()
    {
        return PrefsManager.GetValue<CacheDatas>("cache").CacheData;
    }

    /// <summary>
    /// 弹出一个 列表会删除
    /// </summary>
    /// <returns></returns>
    public CacheData Pop()
    {
        List<CacheData> cachelist = PrefsManager.GetValue<CacheDatas>("cache").CacheData;
        if (cachelist.Count <= 0)
        {
            return null;
        }
        else
        {
            var temp = cachelist[0];
            cachelist.RemoveAt(0);
            CacheDatas datas = new CacheDatas();
            datas.CacheData = cachelist;
            PrefsManager.SetValue("cache", datas);

            return temp;
        }
    }

    public void OnGUI()
    {
        if (GUILayout.Button("存"))
        {
            PrefsManager.SetValue<Transform>("game",transform);
            
        }

        if (GUILayout.Button("弹出"))
        {
           var data =  Pop();
           if (data == null)
           {
               //Debug.Log("所有的已经弹出");
               return;
           }

           //Debug.Log(data.json);
            //Debug.Log(data.path);
            //Debug.Log(data.Par);
           
        }
        
    }
}