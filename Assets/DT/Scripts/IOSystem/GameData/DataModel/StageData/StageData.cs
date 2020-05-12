using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StageData
{
    /// <summary>
    /// 场景名称
    /// </summary>
    public string sceneName;

    /// <summary>
    /// 消费者满意度上限
    /// </summary>
    public float maxConsumer;

    /// <summary>
    /// 消费者满意度初始值
    /// </summary>
    public float startConsumer;

    /// <summary>
    /// 股东满意度上限
    /// </summary>
    public float maxBoss;

    /// <summary>
    /// 股东满意度初始值
    /// </summary>
    public float startBoss;

    /// <summary>
    /// 银行利率
    /// </summary>
    public float bankRate;

    /// <summary>
    /// 初始工人列表
    /// </summary>
    public List<int> startWorker;

    /// <summary>
    /// 初始装备列表
    /// </summary>
    public List<int> startEquip;

    /// <summary>
    /// 消费者质量要求
    /// </summary>
    public int consumerQualityNeed;
}