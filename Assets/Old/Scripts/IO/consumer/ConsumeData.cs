using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameEnum;

[Serializable]
public class ConsumeData 
{
    /// <summary>
    /// 生命值（血量）
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// 存活时间
    /// </summary>
    public float liveTime;

    /// <summary>
    /// 同时生产几个
    /// </summary>
    public int spawnNumber;

    /// <summary>
    /// 击杀获得金钱
    /// </summary>
    public int killMoney;

    /// <summary>
    /// 击杀获得满意度
    /// </summary>
    public int killSatisfy;

    /// <summary>
    /// 存活满意度惩罚
    /// </summary>
    public int liveSatisfy;

    /// <summary>
    /// 消费者名称
    /// </summary>
    public string consumerName;

    /// <summary>
    /// 初始化消费者的数据
    /// </summary>
    public ConsumeData(ConsumerType type)
    {
        ConsumerTypeData data = GameDataMgr.My.GetConsumerTypeDataByType(type);
        maxHealth = 100;
        moveSpeed = 1f;
        liveTime = 10f;
        spawnNumber = 2;
        killMoney = 10;
        killSatisfy = 10;
        liveSatisfy = 10;
        consumerName = "消费者名称";
    }
}
