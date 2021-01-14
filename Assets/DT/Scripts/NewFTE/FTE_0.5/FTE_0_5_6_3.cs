using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_6_3 : BaseGuideStep
{

    public GameObject redpoint;
    public int targetdamege;
    /// <summary>
    /// 当前速率
    /// </summary>
    public int currentRate;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        StageGoal.My.maxRoleLevel = 2;
        
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        redpoint.SetActive( false);
        yield return new WaitForSeconds(2);
    }


    public override bool ChenkEnd()
    {
        if (RoleUpdateInfo.My.currentRole.baseRoleData.level == 1)
        {
            redpoint.SetActive( true);
        }
        else
        {
            redpoint.SetActive( false);
            
        }

        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.level == 1)
                {
                    return false;
                }
                
            }

        }

        missiondatas.data[0].isFinish = true;
        return true;

    }
    
}