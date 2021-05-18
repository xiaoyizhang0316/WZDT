using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasteFactory : BaseServiceSkill
{
    public int tasteBuffId;

    public override void Skill(TradeData data)
    {
        base.Skill(data);
        PlayerData.My.GetMapRoleById(double.Parse(data.targetRole)).tasteBuffList.Add(tasteBuffId);
    }

    public override void SkillOff(TradeData data)
    {
        base.SkillOff(data);
        BaseMapRole role = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole));
        if (role.tasteBuffList.Contains(tasteBuffId))
        {
            role.tasteBuffList.Remove(tasteBuffId);
        }
    }
}
