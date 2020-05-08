using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 学校研究
/// </summary>
public class SchoolResearch : BaseSkill
{
    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        InitBuff();
        CastBuff(baseMapRole, tradeData);
        SkillCost(baseMapRole);
        PlayerData.My.UnLockSelectTradeSkill(10013);
        return true;
    }
}
