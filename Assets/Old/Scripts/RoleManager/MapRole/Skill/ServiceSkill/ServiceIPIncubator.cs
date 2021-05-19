using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IP孵化公司
/// </summary>
public class ServiceIPIncubator : BaseServiceSkill
{
    public override void Skill(TradeData data)
    {
        BaseMapRole role = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole));
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            AddRoleBuff(data);
        }
        else
        {
            // TODO 加钱更多
        }
    }
    
    
}
