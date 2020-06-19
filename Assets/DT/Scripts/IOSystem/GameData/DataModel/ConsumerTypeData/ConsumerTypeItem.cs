using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ConsumerTypeItem
{
    public string consumerType;

    public string typeDesc;

    /// <summary>
    /// 生命值（血量）
    /// </summary>
    public string maxHealth;

    /// <summary>
    /// 移动速度
    /// </summary>
    public string moveSpeed;

    /// <summary>
    /// 存活时间
    /// </summary>
    public string liveTime;

    /// <summary>
    /// 同时生产几个
    /// </summary>
    public string spawnNumber;

    /// <summary>
    /// 击杀获得金钱
    /// </summary>
    public string killMoney;

    /// <summary>
    /// 击杀获得满意度
    /// </summary>
    public string killSatisfy;

    /// <summary>
    /// 存活满意度惩罚
    /// </summary>
    public string liveSatisfy;

    public string bornBuff;
}

[Serializable]
public class ConsumerTypesData
{
    public List<ConsumerTypeItem> consumerTypeSigns = new List<ConsumerTypeItem>();
}

