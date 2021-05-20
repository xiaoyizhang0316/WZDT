using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinksGroupThreshold : BaseFinancialCompanyThreshold
{
    public int targetSeedEffect;
    public override bool Threshold()
    {
        return CheckSeed();
    }

    public override string FailedTip()
    {
        return "场上未有种子商的效果值达到" + targetSeedEffect;
    }

    bool CheckSeed()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.effect >= targetSeedEffect)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
