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
        if (targetNumber >= 3)
        {
            starTwoStatus = true;
        }
        starTwoCondition = "满足传奇蓝领数量" + targetNumber.ToString() + "/3";
    }

    public override void CheckStarThree()
    {
        if (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth >= 1f)
        {
            starThreeStatus = true;
        }
        else
        {
            starThreeStatus = false;
        }
        string number = (StageGoal.My.playerHealth / (float)StageGoal.My.playerMaxHealth * 100).ToString("F2") + "%";
        starThreeCondition = "满意度不低于100%，当前：" + number;
        CheckCheat(); 
    }
}
