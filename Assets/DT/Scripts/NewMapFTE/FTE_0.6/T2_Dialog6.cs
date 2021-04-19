using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog6 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        // delete 20210419
        /*T2_Manager.My.SetDeleteButton(true);
        T2_Manager.My.SetUpdateButton(true);
        T2_Manager.My.SetRoleMaxLevel(2);*/
        BaseMapRole seed = GetSeed();
        if (seed != null)
        {
            TradeManager.My.DeleteTrade(seed.tradeList[0].tradeData.ID);
            T2_Manager.My.HideTradeButton(seed);
        }
    }
    
    BaseMapRole GetSeed()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                return PlayerData.My.MapRole[i];
            }
        }

        return null;
    }
}
