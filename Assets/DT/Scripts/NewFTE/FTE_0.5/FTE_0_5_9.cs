using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_9 : BaseGuideStep
{

    public GameObject nongminLock;

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        nongminLock.SetActive(false);
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }


    public override bool ChenkEnd()
    {
        int count = 0;

        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant&&!PlayerData.My.MapRole[i].isNpc)
            {
                count++;
                   
            }

            if (count >= 2)
            {
                missiondatas.data[0].isFinish = true;
                missiondatas.data[0].currentNum = count;
                return true;

            }
        }

        return false;
    }
}