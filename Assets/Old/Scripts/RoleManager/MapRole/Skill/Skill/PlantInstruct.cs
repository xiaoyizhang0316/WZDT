using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 种植指导
/// </summary>
public class PlantInstruct : BaseSkill
{

    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        InitBuff();
        CastBuff(baseMapRole, tradeData);
        SkillCost(baseMapRole);
        baseMapRole.UnLockPassivitySkill("种植指导_被动");
        return true;
    }
}
