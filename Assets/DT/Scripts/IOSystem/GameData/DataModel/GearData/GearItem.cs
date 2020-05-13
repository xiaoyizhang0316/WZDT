using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class GearItem
{

    public string equipId;

    public string equipName;

    /// <summary>
    /// 品阶
    /// </summary>
    public string level;

    /// <summary>
    /// 效果值
    /// </summary>
    public string effect;

    /// <summary>
    /// 效率值
    /// </summary>
    public string efficiency;

    /// <summary>
    /// 范围值
    /// </summary>
    public string range;

    /// <summary>
    /// 风险抗力
    /// </summary>
    public string riskResistance;

    /// <summary>
    /// 交易成本
    /// </summary>
    public string tradeCost;

    /// <summary>
    /// 成本
    /// </summary>
    public string cost;

    /// <summary>
    /// 弹药容量
    /// </summary>
    public string bulletCapacity;
}

public class GearsData
{
    public List<GearItem> gearItems = new List<GearItem>();
}

