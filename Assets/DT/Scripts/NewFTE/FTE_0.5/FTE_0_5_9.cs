using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_9 : BaseGuideStep
{

    public GameObject nongminLock;

    public GameObject red;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.dealerJC1);
    //    FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.dealerJC2);
        red.SetActive(true);
        nongminLock.SetActive(false);
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        red.SetActive(false);

        yield break;
    }


    public override bool ChenkEnd()
    {
        TradeManager.My.HideAllIcon();
        int count = 0;

        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant&&!PlayerData.My.MapRole[i].isNpc)
            {
                count++;
                   
            }
            missiondatas.data[0].currentNum = count;
            if (count >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
          
                return true;

            }
        }

        return false;
    }
}