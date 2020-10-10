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

[Serializable]
public class StatItemData
{
    public string itemName;
    public int perMin;
    public int count;
    public StatItemType type;

    public StatItemData(string itemName, int perMin, int count, StatItemType type)
    {
        this.itemName = itemName;
        this.perMin = perMin;
        this.count = count;
        this.type = type;
    }

    public override string ToString()
    {
        return "{"+itemName+", "+perMin+"/60s, "+count+", "+type;
    }
}

[Serializable]
public class StatItemDatas
{
    public List<StatItemData> statItemDatas;

    public StatItemDatas(List<StatItemData> statItemDatas)
    {
        this.statItemDatas = statItemDatas;
    }
}

[Serializable]
public class StatItemDatasList
{
    public List<StatItemDatas> statItemDatasList;

    public StatItemDatasList()
    {
        statItemDatasList = new List<StatItemDatas>();
    }
}

public enum StatItemType
{
    TotalIncome,
    ConsumeIncome,
    TotalCost,
    TradeCost,
    NpcIncome,
    OtherIncome,
    BuildCost,
    ExtraCost
}
