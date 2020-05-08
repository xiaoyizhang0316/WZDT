using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

/// <summary>
/// 试验田技能
/// </summary>
public class ExperimentField : BaseSkill
{
    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        InitBuff();
        CastBuff(baseMapRole,tradeData);
        SkillCost(baseMapRole);
        baseMapRole.UnLockPassivitySkill("试验田_被动");
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
