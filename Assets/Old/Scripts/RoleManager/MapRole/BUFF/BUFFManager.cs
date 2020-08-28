using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class BUFFManager : MonoSingleton<BUFFManager>
{
    public List<BaseBuff> buffDatas; 

    /// <summary>
    /// 根据buffId获取对应的buff数据
    /// </summary>
    /// <param name="buffId"></param>
    /// <returns></returns>
    public BaseBuff GetBaseBuffById(int buffId)
    {
        foreach (BaseBuff b in buffDatas)
        {
            if (b.buffId == buffId)
            {
                return b;
            }
        }
        return null;
    }
}
