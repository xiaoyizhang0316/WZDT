using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameEnum;

  [Serializable]
public class ProductData
{
    public ProductData()
    {
        productType = ProductType.Seed;
        Sweetness = 1;
        Crisp = 1;
        Quality = 1;
        Appearance = 1;
      
        unitPrice = 1;
        Quantity = 1;
        Brand = 1;
        time = 60;

    }

    /// <summary>
    /// 检查是否腐烂
    /// </summary>
    public void CheckBirth()
    {
        if (Math.Abs(time - 0f) > 0.01f)
        {
            if (Math.Abs(birthday - TimeManager.My.cumulativeTime) >= time)
            {
                Decay();
            }
        }
    }

    /// <summary>
    /// 腐烂结算
    /// </summary>
    public void Decay()
    {
        if (productType == ProductType.Melon)
        {
            productType = ProductType.DecayMelon;
            birthday = (int)TimeManager.My.cumulativeTime;
            time = 0f;
            //Debug.Log("瓜已腐烂!~");
        }

    }

    /// <summary>
    /// 归属人
    /// </summary>
    //  public string Owner;
    /// <summary>
    /// 种类
    /// </summary>
    public ProductType productType;

     /// <summary>
     /// 甜度
     /// </summary>
     public int Sweetness;

     /// <summary>
     /// 脆度
     /// </summary>
     public int Crisp;

     /// <summary>
     /// 质量
     /// </summary>
     public int Quality;

     /// <summary>
     /// 外观
     /// </summary>
     public int Appearance;

     public double ID;
     
     /// <summary>
     /// 单价 
     /// </summary>
     public int unitPrice;

     /// <summary>
     /// 数量
     /// </summary>
     public int Quantity;

    /// <summary>
    /// 品牌
    /// </summary>
    public int Brand;

    /// <summary>
    /// 生命周期
    /// </summary>
    public float time;

     /// <summary>
     /// 生日
     /// </summary>
     public float birthday;
     
     
}


