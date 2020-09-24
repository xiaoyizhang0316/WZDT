using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level1Controller : BaseLevelController
{

    public override void CheckStarTwo()
    {
        BaseMapRole[] mapRoles = FindObjectsOfType<BaseMapRole>();
        int count = 0;
        for (int i = 0; i < mapRoles.Length; i++)
        {
            if (mapRoles[i].baseRoleData.baseRoleData.level >= 2)
            {
                count++;
                if (count >= 2)
                {
                    starTwoStatus = true;
                    break;
                }
            }
        }
        starTwoCondition = "关卡内任意两个角色达到LV2 : " + count.ToString() + "/2";
    }

    public override void CheckStarThree()
    {
        if (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth >= 0.8f)
        {
            starThreeStatus = true;
        }
        else
        {
            starThreeStatus = false;
        }
        string number = (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth * 100).ToString("##.##") + "%";
        starThreeCondition = "玩家血量不低于80%，当前：" + number;
    }
}
