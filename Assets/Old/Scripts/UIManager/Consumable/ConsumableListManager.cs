using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class ConsumableListManager : MonoSingleton<ConsumableListManager>
{
    public List<ConsumableSign> _signs = new List<ConsumableSign>();

    /// <summary>
    /// 消耗品列表prefab
    /// </summary>
    public GameObject consumablePrb;

    /// <summary>
    /// 列表transform
    /// </summary>
    public Transform scrollViewContent;

    /// <summary>
    /// 拖拽定位
    /// </summary>
    public Transform dragPos;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        for (int i = 0; i < scrollViewContent.childCount; i++)
        {
            Destroy(scrollViewContent.GetChild(i).gameObject);
        }
        foreach (PlayerConsumable p in PlayerData.My.playerConsumables)
        {
          
            GameObject go = Instantiate(consumablePrb, scrollViewContent.position, scrollViewContent.rotation, scrollViewContent);
            go.GetComponent<ConsumableSign>().Init(p);
        }
    }

    /// <summary>
    /// 获得消耗品信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ConsumableSign GetConsubaleSignById(int id)
    {
        foreach (ConsumableSign c in _signs)
        {
            if (c.consumableId == id)
                return c;
        }
        print("---------查不到此消耗品------");
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
