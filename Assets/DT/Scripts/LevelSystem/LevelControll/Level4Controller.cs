using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Controller : BaseLevelController
{

    public override void CheckStarTwo()
    {
        int count = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].isNpc)
            {
                if (PlayerData.My.MapRole[i].npcScript.isCanSee)
                    count++;
            }
        }
        if (count == 11)
            starTwoStatus = true;
        starTwoCondition = "使用广角镜查看所有NPC角色:" + count.ToString() + "/11";
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
