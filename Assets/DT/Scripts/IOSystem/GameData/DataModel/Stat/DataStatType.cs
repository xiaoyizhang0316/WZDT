using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStatType
{

}

public enum IncomeType
{
    Consume,
    Npc,
    Other
}

public enum ExpendType 
{ 
    TradeCosts,
    ProductCosts,
    AdditionalCosts
}

public enum IncomeTpType
{
    Npc,
    Worker,
    Buff
}

public enum CostTpType
{
    Build,
    Mirror,
    Unlock
}

public enum ScoreType
{
    消费者得分,
    口味额外得分,
    金钱得分,
    其他得分
}