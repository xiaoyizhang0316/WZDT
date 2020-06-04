using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level1Controller : BaseLevelController
{

    public override void CheckStarTwo()
    {
        BaseMapRole[] mapRoles = FindObjectsOfType<BaseMapRole>();
        for (int i = 0; i < mapRoles.Length; i++)
        {
            if (mapRoles[i].baseRoleData.baseRoleData.roleType == RoleType.Dealer && mapRoles[i].baseRoleData.baseRoleData.level >= 2)
            {
                starTwoStatus = true;
                break;
            }
        }
        if (starTwoStatus)
            starTwoCondition = "关卡内有一个零售商达到LV2 : 1/1";
        else
            starTwoCondition = "关卡内有一个零售商达到LV2 : 0/1";
    }

    public override void CheckStarThree()
    {
        if (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth > 0.8f)
        {
            starThreeStatus = true;
        }
        else
        {
            starThreeStatus = false;
        }
        string number = (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth * 100).ToString() + "%";
        starThreeCondition = "玩家血量不低于80%，当前：" + number;
    }
}
