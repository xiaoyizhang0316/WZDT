﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_13 : BaseGuideStep
{

    public BaseMapRole role;
    public BaseMapRole role1;
    public BaseMapRole nongmin;
    public BaseMapRole maoyi;
    
    

    int time;

    public int shengyuTime;
    public Text info; 
    public int targetdamege; 
    
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        time = StageGoal.My.timeCount;
        role.warehouse.Clear();

       for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
                    {
                        if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
                        {

                            maoyi = PlayerData.My.MapRole[i];
                        }
                    }
       for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
       {
           if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant&&!PlayerData.My.MapRole[i].isNpc)
           {

               nongmin = PlayerData.My.MapRole[i];
           }
       }

        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }


    public override bool ChenkEnd()
    {

        for (int i = 0; i < role.warehouse.Count; i++)
        {
            if (role.warehouse[i].damage <targetdamege)
            {
                role.warehouse.Remove(role.warehouse[i]);
            }
        }
        for (int i = 0; i < role1.warehouse.Count; i++)
        {
            if (role1.warehouse[i].damage <targetdamege)
            {
                role1.warehouse.Remove(role1.warehouse[i]);
            }
        }

        info.text = "剩余时间 : "+(shengyuTime- (StageGoal.My.timeCount-time)) +"质监站1剩余："+(role.baseRoleData.bulletCapacity-role.warehouse.Count)
                    +"           质监站2剩余："+(role1.baseRoleData.bulletCapacity-role1.warehouse.Count)
            ;
        if ((StageGoal.My.timeCount - time) % shengyuTime == 0)
        {
            role.warehouse.Clear();
            role1.warehouse.Clear();
        }
  
        if ( role.warehouse.Count > missiondatas.data[0].maxNum&& role1.warehouse.Count > missiondatas.data[1].maxNum)
        {
            missiondatas.data[0].isFinish = true; 
      
        }


        if (TradeManager.My.CheckTwoRoleHasTrade(nongmin.baseRoleData, maoyi.baseRoleData))
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