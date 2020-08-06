using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8Controller : BaseLevelController
{
    public override void CheckStarTwo()
    {
        if (StageGoal.My.playerGold < -5000)
        {
            starTwoStatus = false;
            CancelInvoke("CheckStarTwo");
            return;
        }
        starTwoStatus = true;
        starTwoCondition = "资产从未低于-20000¥";
    }

    public override void CheckStarThree()
    {
        float per = (StageGoal.My.npcIncome + StageGoal.My.otherIncome) / (float)StageGoal.My.totalIncome;
        if ( per > 0.3f)
        {
            starThreeStatus = true;
        }
        else
            starThreeStatus = false;
        starThreeCondition = "来自非消费者的收入占总收入30%以上，当前:" + per.ToString("##.##");
        CheckCheat();
    }
}
