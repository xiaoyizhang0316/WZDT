using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_5_6_2 : BaseGuideStep
{
  
 

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
     
        yield return new WaitForSeconds(1f);
      
    }

    public override IEnumerator StepEnd()
    {
      
       yield break; 
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
            missiondatas.data[0].currentNum =count;
        }
        if (missiondatas.data[0].isFinish )
        {
            return true;
        }

        return false;
    }
 
}
