using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_5_6_1 : BaseGuideStep
{
  
 

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.seerJC2);
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                PlayerData.My.MapRole[i].HideTradeButton(false);
            }

            
        }  
        yield return new WaitForSeconds(1f);
      
    }

    public override IEnumerator StepEnd()
    {
      
        yield return new WaitForSeconds(2);

    }


   
    public override bool ChenkEnd()
    {
        int count = 0;

        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                count++;
            }

            
        }  
        if (count >= 2)
        {
            missiondatas.data[0].isFinish = true;
            missiondatas.data[0].currentNum =count - 1;
        }
        if (missiondatas.data[0].isFinish )
        {
            return true;
        }

        return false;
    }
 
}
