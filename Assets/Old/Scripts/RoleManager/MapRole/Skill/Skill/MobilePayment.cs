using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移动支付
/// </summary>
public class MobilePayment : BaseSkill
{
    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        InitBuff();
        CastBuff(baseMapRole, tradeData);
        SkillCost(baseMapRole);
        return true;
    }

}
