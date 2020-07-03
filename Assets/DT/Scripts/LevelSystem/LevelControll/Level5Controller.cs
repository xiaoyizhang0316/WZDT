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

    public override void CountPutRole(Role role)
    {
        if (role.baseRoleData.roleType == RoleType.Dealer)
            putRoleNumber++;
    }

    public override void CheckStarTwo()
    {
        if (putRoleNumber >= 6)
        {
            starTwoStatus = false;
            CancelInvoke("CheckStarTwo");
            return;
        }
        starTwoStatus = true;
        starTwoCondition = "放置不多于6个零售商,当前：" + putRoleNumber.ToString() + "/6";
    }

    public override void CheckStarThree()
    {
        if (targetNumber >= 12)
        {
            starThreeStatus = true;
        }
        starThreeCondition = "满足传奇金领数量" + targetNumber.ToString() + "/12";
    }
}
