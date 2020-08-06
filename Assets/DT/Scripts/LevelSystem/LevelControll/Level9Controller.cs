using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9Controller : BaseLevelController
{

    public List<MapSign> lockLandList;

    public BossConsumer targetBoss;

    public override void CheckStarOne()
    {
        if (targetBoss == null)
        {
            targetBoss = FindObjectOfType<BossConsumer>();
            if (targetBoss == null)
            {
                starOneStatus = false;
                starOneCondition = "首领消费者满足超过8次,当前：1";
                return;
            }

        }
        if (targetBoss.killCount >= 8)
        {
            starOneStatus = true;
        }
        else
        {
            starOneStatus = false;
        }
        starOneCondition = "首领消费者满足超过8次,当前：" + targetBoss.killCount.ToString();
    }

    public override void CheckStarTwo()
    {
        if (targetBoss == null)
        {
            targetBoss = FindObjectOfType<BossConsumer>();
            if (targetBoss == null)
            {
                starTwoStatus = false;
                starTwoCondition = "首领消费者满足超过15次,当前：1";
                return;
            }

        }
        if (targetBoss.killCount >= 15)
        {
            starTwoStatus = true;
        }
        else
        {
            starTwoStatus = false;
        }
        starTwoCondition = "首领消费者满足超过15次,当前：" + targetBoss.killCount.ToString();
    }

    public override void CheckStarThree()
    {
        if (targetBoss == null)
        {
            targetBoss = FindObjectOfType<BossConsumer>();
            if (targetBoss == null)
            {
                starThreeStatus = false;
                starThreeCondition = "首领消费者满足超过22次,当前：1";
                return;
            }

        }
        if (targetBoss.killCount >= 22)
        {
            starThreeStatus = true;
        }
        else
        {
            starThreeStatus = false;
        }
        starThreeCondition = "首领消费者满足超过22次,当前：" + targetBoss.killCount.ToString();
        CheckCheat();
    }
}
