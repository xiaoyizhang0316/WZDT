using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level6Controller : BaseLevelController
{
    public override void CheckStarTwo()
    {
        int count = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].isNpc)
            {
                if (!PlayerData.My.MapRole[i].npcScript.isLock)
                    count++;
            }
        }
        if (count == 11)
            starTwoStatus = true;
        starTwoCondition = "使用科技值解锁所有角色，当前：" + count.ToString() + "/11";
    }

    public override void CheckStarThree()
    {
        if (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth > 0.6f)
        {
            starThreeStatus = true;
        }
        else
        {
            starThreeStatus = false;
        }
        string number = (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth * 100).ToString() + "%";
        starThreeCondition = "满意度不低于60%，当前：" + number;
    }

}
