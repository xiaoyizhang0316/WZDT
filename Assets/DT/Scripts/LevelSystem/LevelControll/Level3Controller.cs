﻿using System.Collections;
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
        if (targetNumber >= 5)
        {
            starTwoStatus = true;
        }
        starTwoCondition = "满足传奇蓝领数量" + targetNumber.ToString() + "/5";
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
