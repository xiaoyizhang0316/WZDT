using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinksGroupThreshold : BaseFinancialCompanyThreshold
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

    int GetPeasantCount()
    {
        int count = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant)
            {
                count++;
            }
        }

        return count;
    }
}
