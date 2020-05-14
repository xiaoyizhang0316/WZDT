﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;


public class ConsumerTypeData
{
    /// <summary>
    /// 消费者类型
    /// </summary>
    public ConsumerType consumerType;

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
}


