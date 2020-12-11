using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasteFactory : BaseExtraSkill
{
    public int tasteBuffId;

    public override void SkillOn(TradeSign sign)
    {
        base.SkillOn(sign);
        PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole)).tasteBuffList.Add(tasteBuffId);
    }

    public override void SkillOff(TradeSign sign)
    {
        base.SkillOff(sign);
        BaseMapRole role = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole));
        if (role.tasteBuffList.Contains(tasteBuffId))
        {
            role.tasteBuffList.Remove(tasteBuffId);
        }
    }
}
