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
        if (StageGoal.My.totalPauseTime <= 180)
        {
            starThreeStatus = true;
        }
        else
        {
            starThreeStatus = false;
        }
        starThreeCondition = "累计暂停时间不超过180秒，当前：" + StageGoal.My.totalPauseTime.ToString();
        CheckCheat();
    }

}
