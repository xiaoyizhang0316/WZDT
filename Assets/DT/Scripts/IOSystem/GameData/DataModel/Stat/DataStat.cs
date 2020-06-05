using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// 货币成本
    /// </summary>
    public int cost;
    /// <summary>
    /// 交易成本
    /// </summary>
    public int tradeCost;
    /// <summary>
    /// 金钱
    /// </summary>
    public int totalGold;

    //public string timeStamp;

    public DataStat(int blood, int score, int cost, int tradeCost, int totalGold)
    {
        this.blood = blood;
        this.score = score;
        this.cost = cost;
        this.tradeCost = tradeCost;
        this.totalGold = totalGold;
        //this.timeStamp = timeStamp;
    }
}
