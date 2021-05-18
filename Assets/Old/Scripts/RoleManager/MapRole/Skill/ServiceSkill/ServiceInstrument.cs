using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceInstrument : BaseServiceSkill
{
    public override void Skill(TradeData data)
    {
        Role start = PlayerData.My.GetRoleById(double.Parse(data.startRole));
        base.Skill(data);
    }
}
