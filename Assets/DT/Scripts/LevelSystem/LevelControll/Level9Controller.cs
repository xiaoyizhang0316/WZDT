using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9Controller : BaseLevelController
{
    public override void CheckStarTwo()
    {
        if (StageGoal.My.tradeCost > 100000)
        {
            starTwoStatus = false;
            CancelInvoke("CheckStarTwo");
            return;
        }
        starTwoStatus = true;
        starTwoCondition = "累计交易成本不高于100000";
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
        string number = (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth * 100).ToString("##.##") + "%";
        starThreeCondition = "满意度不低于60%，当前：" + number;
        CheckCheat();
    }
}
