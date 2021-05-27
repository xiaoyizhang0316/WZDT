using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工商技能
/// </summary>
public class ServiceTradeOffice : BaseServiceSkill
{
    public int lastTurnIncome;

    public override void Skill(TradeData data)
    {
        var target = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole));
        if (target.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant)
        {
            Debug.Log("工商 - 农民");
            target.GetComponent<RoleTransition>().ActiveTransition(GameEnum.RoleType.PickingGarden,
                new TransitionCause(CauseType.Trade, data.ID, role, data), out var active);
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

    public override void SkillOff(TradeData data)
    {
        var target = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole));

        if (target.GetComponent<RoleTransition>())
        {
            if (target.GetComponent<RoleTransition>().startRoleType == GameEnum.RoleType.Peasant)
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
}