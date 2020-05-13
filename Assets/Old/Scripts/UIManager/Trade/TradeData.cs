using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using System;

[Serializable]
public class TradeData
{
    /// <summary>
    /// 发起者
    /// </summary>
    public string startRole;

    /// <summary>
    /// 承受者
    /// </summary>
    public string endRole;

    /// <summary>
    /// 施法者
    /// </summary>
    public string castRole;

    /// <summary>
    /// 目标
    /// </summary>
    public string targetRole;

    /// <summary>
    /// 选择的收支方式
    /// </summary>
    public SZFSType selectSZFS;

    /// <summary>
    /// 选择的现金流结构
    /// </summary>
    public CashFlowType selectCashFlow;

    /// <summary>
    /// 是否免费
    /// </summary>
    public bool isFree;

    /// <summary>
    /// ID
    /// </summary>
    public int ID;

    /// <summary>
    /// 分成比例
    /// </summary>
    public float payPer;
}
