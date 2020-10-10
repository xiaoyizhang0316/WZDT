using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level5Controller : BaseLevelController
{

    public override void CountKillNumber(ConsumeSign sign)
    {
        List<ConsumerType> list = new List<ConsumerType>() { ConsumerType.GoldencollarLegendary };
        if (list.Contains(sign.consumerType))
        {
            targetNumber++;
        }
    }

    public override void CheckStarTwo()
    {
        int count = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (!PlayerData.My.MapRole[i].isNpc)
            {
                count++;
                if (count > 6)
                {
                    starTwoStatus = false;
                    CancelInvoke("CheckStarTwo");
                    starTwoCondition = "放置不多于6个角色，当前：已失败";
                    return;
                }
            }
        }
        starTwoStatus = true;
        starTwoCondition = "放置不多于6个角色，当前：" + count.ToString() + "/6";
    }

    public override void CheckStarThree()
    {
        int count = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (!PlayerData.My.MapRole[i].isNpc && PlayerData.My.MapRole[i].baseRoleData.baseRoleData.level == 5)
                count++;
        }
        if (count >= 3)
            starThreeStatus = true;
        else
            starThreeStatus = false;
        starThreeCondition = "至少3个内部角色升级到5级，当前:" + count.ToString();
        CheckCheat();
    }
}
