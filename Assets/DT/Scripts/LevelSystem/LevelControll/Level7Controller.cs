using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level7Controller : BaseLevelController
{
    public override void CountKillNumber(ConsumeSign sign)
    {
        if (sign.lastHitType == DT.Fight.Bullet.BulletType.summon)
        {
            targetNumber++;
        }
    }

    public override void CheckStarTwo()
    {
        if (targetNumber >= 80)
            starTwoStatus = true;
        starTwoCondition = "用罐头满足80个消费者，当前：" + targetNumber.ToString();
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
