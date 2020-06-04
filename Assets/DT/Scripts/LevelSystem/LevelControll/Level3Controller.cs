using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level3Controller : BaseLevelController
{

    public override void CountKillNumber(ConsumeSign sign)
    {
        List<ConsumerType> list = new List<ConsumerType>() { ConsumerType.BluecollarLegendary};
        if (list.Contains(sign.consumerType))
        {
            targetNumber++;
        }
    }

    public override void CheckStarTwo()
    {
        if (targetNumber >= 2)
        {
            starTwoStatus = true;
        }
    }

    public override void CheckStarThree()
    {
        if (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth > 0.7f)
            starThreeStatus = true;
    }
}
