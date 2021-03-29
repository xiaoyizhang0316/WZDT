using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class T0_5_9 : BaseGuideStep
{

    
    public override IEnumerator StepStart()
    {
       FTE_0_5Manager.My.SetDeleteButton(true);
       FTE_0_5Manager.My.SetRoleInfoDown();
      TradeManager.My.ShowAllIcon();
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        
       
        yield return new WaitForSeconds(1);
         }


    public override bool ChenkEnd()
    {
        int count = 0;

        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant &&!PlayerData.My.MapRole[i].isNpc)
            {
                count++;
            }
            
        }

        if (count <= 1)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }
     else
        {
            return false;
        }
    }
}