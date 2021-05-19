using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 健康机构
/// </summary>
public class ServiceHealthcare : BaseServiceSkill
{
    public float rate;
    public override void Skill(TradeData data)
    {
        BaseLevelController.My.satisfyRate += rate;
    }

    public override void SkillOff(TradeData data)
    {
        BaseLevelController.My.satisfyRate -= rate;
    }
}
