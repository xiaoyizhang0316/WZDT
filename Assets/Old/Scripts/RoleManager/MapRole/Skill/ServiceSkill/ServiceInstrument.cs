using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceInstrument : BaseServiceSkill
{
    
    public override void Skill(TradeData data)
    {
        var target = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole));
        var isTransition = CheckTransition(target, out var transitionType);
        if (isTransition)
        {
            target.GetComponent<RoleTransition>().ActiveTransition(transitionType, new TransitionCause(CauseType.Trade, data.ID, role, data), out var active);
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
        }
        else
        {
            base.Skill(data);
        }
    }

    public override void SkillOff(TradeData data)
    {
        var target = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole));
        var isTransition = CheckTransition(target, out var transitionType);
        if (isTransition)
        {
            if (target.GetComponent<RoleTransition>())
            {
                if (target.GetComponent<RoleTransition>().IsTransition)
                {
                    if (target.GetComponent<RoleTransition>().CheckIsThisTradeCause(role))
                    {
                        target.GetComponent<RoleTransition>().Restore();
                    }
                    else
                    {
                        target.GetComponent<RoleTransition>().RemoveFailedCauseData(data);
                        base.SkillOff(data);
                    }
                }
                else
                {
                    target.GetComponent<RoleTransition>().RemoveFailedCauseData(data);
                    base.SkillOff(data);
                }
            }
            else
            {
                base.SkillOff(data);
            }
        }
        else
        {
            base.SkillOff(data);
        }
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
