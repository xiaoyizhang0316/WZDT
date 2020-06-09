using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DT.Fight.Bullet;
using static GameEnum;

  [Serializable]
public class ProductData
{
    public ProductData()
    {
    }

    public ProductData( ProductData data)
    {
        bulletType = data.bulletType;
        damage = data.damage;
        loadingSpeed = data.loadingSpeed;
        buffList =new List<int>() ;
        buffList.AddRange(data.buffList);
        buffMaxCount = data.buffMaxCount;
    }

    public BulletType bulletType;

    /// <summary>
    /// 伤害
    /// </summary>
    public float damage;

    /// <summary>
    /// 装弹速度
    /// </summary>
    public float loadingSpeed;

    /// <summary>
    /// BuffList
    /// </summary>
    public List<int> buffList = new List<int>();

    /// <summary>
    /// Buff上限
    /// </summary>
    public int buffMaxCount;

    public void AddBuff(int buffId)
    {
        if (buffList.Count >= buffMaxCount)
        {
            return;
        }
        else
        {
            if(!buffList.Contains(buffId))
            {
                //BuffData data = GameDataMgr.My.GetBuffDataByID(buffId);
                //if(data.bulletBuffType == BulletBuffType.Element)
                //{
                //    for (int i = 0; i < buffList.Count; i++)
                //    {
                //        BuffData bData = GameDataMgr.My.GetBuffDataByID(buffList[i]);
                //        if (bData.bulletBuffType == BulletBuffType.Element)
                //        {
                //            buffList.RemoveAt(i);
                //        }
                //    }
                //}
                buffList.Add(buffId);
            }

        }
    }

    /// <summary>
    /// 当改变上限的时候 调用
    /// </summary>
    public void CheckBuff()
    {
        for (int i = 0; i <int.MaxValue; i++)
        {
            if (buffMaxCount < buffList.Count)
            {
                buffList.RemoveAt(buffList.Count-1);
                if (buffMaxCount >= buffList.Count)
                {
                    return;
                }
            }
        }
      
    }
}


