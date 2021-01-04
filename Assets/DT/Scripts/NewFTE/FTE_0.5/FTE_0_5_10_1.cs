using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_10_1 : BaseGuideStep
{

    
    public override IEnumerator StepStart()
    {
      TradeManager.My.ShowAllIcon();
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        FTE_0_5Manager.My.DownRole( FTE_0_5Manager.My.seerJC1);
        FTE_0_5Manager.My.DownRole( FTE_0_5Manager.My.seerJC2);
       
        yield return new WaitForSeconds(1);
        PlayerData.My.DeleteRole(FTE_0_5Manager.My.seerJC1.GetComponent<BaseMapRole>().baseRoleData.ID);
        PlayerData.My.DeleteRole(FTE_0_5Manager.My.seerJC2.GetComponent<BaseMapRole>().baseRoleData.ID);
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
            missiondatas.data[1].isFinish = true;
        }


        int count1 = 0;

        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                count1++;
            }
            
        }

        if (count1 <= 1)
        {
            missiondatas.data[0].isFinish = true;
        }


        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish )
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }
}