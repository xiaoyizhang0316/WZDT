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
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == RoleType.Dealer)
            {
                if (!PlayerData.My.MapRole[i].isNpc)
                {
                    starThreeStatus = false;
                    CancelInvoke("CheckStarThree");
                    return;
                }
            }
        }
        starThreeStatus = true;
        starThreeCondition = "不放置自有的零售商";
        CheckCheat();
    }
}
