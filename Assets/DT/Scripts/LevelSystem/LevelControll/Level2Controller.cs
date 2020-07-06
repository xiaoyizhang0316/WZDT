using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level2Controller : BaseLevelController
{

    public override void CountKillNumber(ConsumeSign sign)
    {
        List<ConsumerType> list = new List<ConsumerType>() { ConsumerType.BluecollarRare };
        if (list.Contains(sign.consumerType))
        {
            targetNumber++;
        }
    }

    public override void CheckStarTwo()
    {
        if (targetNumber >= 28)
        {
            starTwoStatus = true;
        }
        starTwoCondition = "满足中级蓝领数量:" + targetNumber.ToString() + "/28";
    }

    public override void CheckStarThree()
    {
        int count = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (!PlayerData.My.MapRole[i].isNpc)
            {
                count++;
                if (count > 6)
                {
                    starThreeStatus = false;
                    CancelInvoke("CheckStarThree");
                    starThreeCondition = "放置不多于6个角色，当前：" + count.ToString() + "/6";
                    return;
                }
            }
        }
        starThreeStatus = true;
        starThreeCondition = "放置不多于6个角色，当前：" + count.ToString() + "/6";
    }
}
