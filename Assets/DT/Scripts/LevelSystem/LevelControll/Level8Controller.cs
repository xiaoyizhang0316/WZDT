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
        starTwoCondition = "资产从未低于-5000¥";
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
