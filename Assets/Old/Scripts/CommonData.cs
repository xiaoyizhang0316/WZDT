using System;
using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class CommonData : MonoSingletonDontDestroy<CommonData>
{
    /// <summary>
    /// 当前主场景UICanvas
    /// </summary>
    public GameObject canvas;

    /// <summary>
    /// 当前Cd预制体
    /// </summary>
    public GameObject cdSprite;
    
    /// <summary>
    /// 质量认可度
    /// </summary>
    public int qualityRecognition;

    /// <summary>
    /// 种子
    /// </summary>
    public Sprite seetSprite;
    /// <summary>
    /// 瓜
    /// </summary>
    public Sprite  melonSprite;

    /// <summary>
    /// 游客创建预制体
    /// </summary>
    public GameObject comsumerPos;

    /// <summary>
    /// 股东满意度
    /// </summary>
    public float BossValue = 100;

    public GameObject hand;

    public GameObject RoleTF;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public   double GetTimestamp(DateTime d)
    {
        TimeSpan ts = d.ToUniversalTime() - new DateTime(1970, 1, 1);
        return ts.TotalMilliseconds;     //精确到毫秒
    }
    /// <summary>
    /// Sprite公用路径
    /// </summary>
    public string SpritePath = "Sprite/";

    /// <summary>
    /// Prefabs公用路径
    /// </summary>
    public string PrefabPath = "Prefabs/";
}
