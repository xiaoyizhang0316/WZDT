using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class EquipData
{
    public int ID;

    /// <summary>
    /// 品阶
    /// </summary>
    public int ProductOrder;

    /// <summary>
    /// 产能
    /// </summary>
    public int capacity;

    /// <summary>
    /// 效率
    /// </summary>
    public int efficiency;

    /// <summary>
    /// 质量
    /// </summary>
    public int quality;

    /// <summary>
    /// 品牌
    /// </summary>
    public int brand;

    /// <summary>
    /// 甜度加成
    /// </summary>
    public int sweetnessAdd;

    /// <summary>
    /// 脆度加成
    /// </summary>
    public int BrittlenessAdd;

    /// <summary>
    /// 固定成本
    /// </summary>
    public int fixedCost;
 
    /// <summary>
    /// 每月成本
    /// </summary>
    public int costMonth;

    /// <summary>
    /// 风险加成
    /// </summary>
    public int riskAdd;

    /// <summary>
    /// 搜寻加成
    /// </summary>
    public int searchAdd;

    /// <summary>
    /// 议价加成
    /// </summary>
    public int bargainAdd;    

    /// <summary>
    /// 交付加成
    /// </summary>
    public int DealsBonus;

    /// <summary>
    /// 当前是否正在使用
    /// </summary>
    public bool isUsed;
}
[Serializable]
public class EquipDatas
{
    public List<EquipData>_equipDatas = new List<EquipData>();
    
}