using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceInstrument : BaseServiceSkill
{
    public override void Skill(TradeData data)
    {
        var target = role.baseRoleData.ID.ToString().Equals(data.startRole)?PlayerData.My.GetMapRoleById(double.Parse(data.endRole)):
            PlayerData.My.GetMapRoleById(double.Parse(data.startRole));
        var isTransition = CheckTransition(target, out var transitionType);
        if (isTransition)
        {
            target.GetComponent<RoleTransition>().ActiveTransition(transitionType, new TransitionCause(CauseType.Trade, data.ID), out var active);
            if (!active)
            {
                base.Skill(data);
            }
        }
        else
        {
            base.Skill(data);
        }
    }

    bool CheckTransition(BaseMapRole role, out GameEnum.RoleType transitionType)
    {
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant|| 
            role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.JuiceFactory)
        {
            transitionType = GameEnum.RoleType.BeverageCompany;
            return true;
        }

        transitionType = GameEnum.RoleType.Seed;
        return false;
    }
}
