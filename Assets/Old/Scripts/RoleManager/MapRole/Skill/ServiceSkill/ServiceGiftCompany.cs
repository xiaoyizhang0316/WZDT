using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceGiftCompany : BaseServiceSkill
{
    public float damageRate;
    public override void Skill(TradeData data)
    {
        BaseLevelController.My.tasteDamageRate += damageRate;
    }

    public override void SkillOff(TradeData data)
    {
        BaseLevelController.My.tasteDamageRate -= damageRate;
    }
}
