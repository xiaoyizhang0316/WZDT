using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Controller : BaseLevelController
{

    public override void CheckStarTwo()
    {
        if (targetNumber >= 2)
        {
            starTwoStatus = true;
        }
    }

    public override void CheckStarThree()
    {
        if (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth > 0.8f)
            starThreeStatus = true;
    }
}
