using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Controller : BaseLevelController
{

    public override void CheckStarTwo()
    {
        starTwoStatus = true;
        foreach (TradeSign sign in TradeManager.My.tradeList.Values)
        {
            if (!sign.isTradeSettingBest())
            {
                starTwoStatus = false;
                break;
            }
        }
        starTwoCondition = "所有交易设置为最优设置";
    }

    public override void CheckStarThree()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                if (!PlayerData.My.MapRole[i].isNpc)
                {
                    starThreeStatus = false;
                    CancelInvoke("CheckStarThree");
                    return;
                }
            }
        }
        starThreeStatus = true;
        starThreeCondition = "不放置自有的种子商";
    }
}
