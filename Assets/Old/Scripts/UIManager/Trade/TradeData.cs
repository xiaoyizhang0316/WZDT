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
    /// 技能第三方目标（如果有的话）
    /// </summary>
    public string thirdPartyRole;

    /// <summary>
    /// 选择的交易方式
    /// </summary>
    public string selectJYFS;

    /// <summary>
    /// 选择的目的地
    /// </summary>
    public TradeDestinationType selectTradeDestination;

    /// <summary>
    /// 选择的收支方式
    /// </summary>
    public SZFSType selectSZFS;

    /// <summary>
    /// 选择的现金流结构
    /// </summary>
    public CashFlowType selectCashFlow;

    /// <summary>
    /// 选择的产品
    /// </summary>
    public ProductType selectProduct;

    /// <summary>
    /// 是否免费
    /// </summary>
    public bool isFree;

    /// <summary>
    /// 付钱方
    /// </summary>
    public string payRole;

    /// <summary>
    /// 收钱方
    /// </summary>
    public string receiveRole;

    /// <summary>
    /// ID
    /// </summary>
    public int ID;

    /// <summary>
    /// 分成比例
    /// </summary>
    public float payPer;
}
