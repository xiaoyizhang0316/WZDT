using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LendMoney_Large : BaseSkill
{

    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        InitBuff();
        CastBuff(baseMapRole, tradeData);
        SkillCost(baseMapRole);
        return true;
    }
}
