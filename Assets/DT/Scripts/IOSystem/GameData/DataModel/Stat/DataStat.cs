using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataStat
{
    /// <summary>
    /// 血量
    /// </summary>
    public int blood;
    /// <summary>
    /// 分数
    /// </summary>
    public int score;

    /// <summary>
    /// 总收入
    /// </summary>
    public int totalIncome;

    /// <summary>
    /// 消费收入
    /// </summary>
    public int totalConsumeIncome;

    /// <summary>
    /// 总成本
    /// </summary>
    public int totalCost;

    /// <summary>
    /// 交易成本 
    /// </summary>
    public int tradeCost;

    /// <summary>
    /// 货币成本
    /// </summary>
    public int monthlyCost;
    /// <summary>
    /// 剩下的钱
    /// </summary>
    public int restMoney;
    public DataStat(int blood, int score, 
        int totalIncome, int totalConsumeIncome, 
        int totalCost, int tradeCost, int monthlyCost,
        int restMoney)
    {
        this.blood = blood;
        this.score = score;
        this.totalIncome = totalIncome;
        this.totalConsumeIncome = totalConsumeIncome;
        this.totalCost = totalCost;
        this.tradeCost = tradeCost;
        this.monthlyCost = monthlyCost;
        this.restMoney = restMoney;
    }
}
