using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceInstrument : BaseServiceSkill
{
    private bool isActive;
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
                if (!_unChangeData.ContainsKey(data.ID))
                {
                    _unChangeData.Add(data.ID,data);
                }
                base.Skill(data);
            }
            else
            {
                if (_unChangeData.ContainsKey(data.ID))
                {
                    _unChangeData.Remove(data.ID);
                }
            }

            isActive = active;
        }
        else
        {
            base.Skill(data);
        }
    }

    public override void SkillOff(TradeData data)
    {
        if(!isActive)
            base.SkillOff(data);
    }

    bool CheckTransition(BaseMapRole role, out GameEnum.RoleType transitionType)
    {
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant|| 
            role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.JuiceFactory)
        {
            transitionType = GameEnum.RoleType.DrinksCompany;
            return true;
        }

        transitionType = GameEnum.RoleType.Seed;
        return false;
    }
}
