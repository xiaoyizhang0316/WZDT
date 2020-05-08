using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorkerItem
{

    public string workerId;


    /// <summary>
    /// 品阶
    /// </summary>
    public string level;

    /// <summary>
    /// 产能
    /// </summary>
    public string capacity;

    /// <summary>
    /// 效率
    /// </summary>
    public string efficiency;

    /// <summary>
    /// 质量
    /// </summary>
    public string quality;

    /// <summary>
    /// 品牌
    /// </summary>
    public string brand;

    /// <summary>
    /// 甜度加成
    /// </summary>
    public string sweetnessAdd;

    /// <summary>
    /// 脆度加成
    /// </summary>
    public string crispAdd;

    /// <summary>
    /// 固定成本
    /// </summary>
    public string fixedCost;

    /// <summary>
    /// 每月成本
    /// </summary>
    public string costMonth;

    /// <summary>
    /// 风险加成
    /// </summary>
    public string riskAdd;

    /// <summary>
    /// 搜寻加成
    /// </summary>
    public string searchAdd;

    /// <summary>
    /// 议价加成
    /// </summary>
    public string bargainAdd;

    /// <summary>
    /// 交付加成
    /// </summary>
    public string deliverAdd;

    /// <summary>
    /// PDP性格
    /// </summary>
    public string PDP;
}

[Serializable]
public class WorkersData
{
    public List<WorkerItem> workerItems = new List<WorkerItem>();
}
