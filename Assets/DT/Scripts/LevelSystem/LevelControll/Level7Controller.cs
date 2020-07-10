using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level7Controller : BaseLevelController
{
    public override void CountKillNumber(ConsumeSign sign)
    {
        if (sign.lastHitType == DT.Fight.Bullet.BulletType.Leaser)
        {
            targetNumber++;
        }
    }

    public override void CheckStarTwo()
    {
        if (targetNumber >= 10)
            starTwoStatus = true;
        starTwoCondition = "用罐头满足10个消费者，当前：" + targetNumber.ToString();
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
    }
}
