using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小额贷款技能
/// </summary>
public class LendMoney_Small : BaseSkill
{
    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        InitBuff();
        CastBuff(baseMapRole, tradeData);
        SkillCost(baseMapRole);
        return true;
    }
}
