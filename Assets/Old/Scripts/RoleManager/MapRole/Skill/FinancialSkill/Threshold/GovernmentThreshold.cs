using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentThreshold : BaseFinancialCompanyThreshold
{
    public int targetPeasantNum;
    public override bool Threshold()
    {
        return GetPeasantCount() >= targetPeasantNum;
    }

    public override string FailedTip()
    {
        return "场上农民数量未达到" + targetPeasantNum;
    }

    public override string ThresholdTip()
    {
        return "场上至少存在" + targetPeasantNum + "个农民";
    }

    int GetPeasantCount()
    {
        int count = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant||
                PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.PickingGarden)
            {
                count++;
            }
        }

        return count;
    }
}
