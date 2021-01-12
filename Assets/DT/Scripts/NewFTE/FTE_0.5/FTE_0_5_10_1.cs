﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_10_1 : BaseGuideStep
{

    
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().delete.interactable = true;

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