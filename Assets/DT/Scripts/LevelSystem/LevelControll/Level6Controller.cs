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
        starTwoCondition = "使用Mega值解锁所有角色，当前：" + count.ToString() + "/11";
    }

    public override void CheckStarThree()
    {
        if (StageGoal.My.isInTurnCreateTrade)
        {
            starThreeStatus = false ;
        }
        else
        {
            starThreeStatus = true;
        }
        starThreeCondition = "开始阶段时不配置交易";
        CheckCheat();
    }

}
