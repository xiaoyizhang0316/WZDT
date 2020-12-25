﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_15 : BaseGuideStep
{
    public BaseMapRole maoyi;
    public BaseMapRole lingshou;

    public GameObject roleImage;

    public int count;
    public int time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        InvokeRepeating("Addxiaofei",1,time);
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
            {
                maoyi = PlayerData.My.MapRole[i];
            }
        }

        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer &&
                !PlayerData.My.MapRole[i].isNpc)
            {
                lingshou = PlayerData.My.MapRole[i];
            }

        }
        yield return new WaitForSeconds(1f);

    }

    public void Addxiaofei()
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(GameEnum.ConsumerType.OldpaoNormal, count));
        
        }

    public override IEnumerator StepEnd()
    {
        CancelInvoke("Addxiaofei");
        PlayerData.My.GetNewGear(22301);
        PlayerData.My.GetNewGear(22302);
        PlayerData.My.GetNewGear(22303);
        PlayerData.My.GetNewGear(22304); 

        yield break;
    }

    public override bool ChenkEnd()
    {
        if (TradeManager.My.CheckTwoRoleHasTrade(maoyi.baseRoleData, lingshou.baseRoleData))
        {
            missiondatas.data[0].isFinish = true;
        }

        if (StageGoal.My.killNumber > missiondatas.data[1].maxNum)
        {
            missiondatas.data[1].isFinish = true;
        }
        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
   
}
 