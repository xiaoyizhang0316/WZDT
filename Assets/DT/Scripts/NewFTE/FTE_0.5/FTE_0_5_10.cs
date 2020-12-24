using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_10 : BaseGuideStep
{

    public BaseMapRole role;

    public BaseMapRole roleseed;
    public BaseMapRole roleseed1;
    public BaseMapRole rolenongmin;
    public BaseMapRole npc1;
    public BaseMapRole npc2;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                roleseed = PlayerData.My.MapRole[i];
            }
        }
        PlayerData.My.DeleteRole(roleseed.baseRoleData.ID);
        PlayerData.My.DeleteRole(npc1.baseRoleData.ID);
        PlayerData.My.DeleteRole(npc2.baseRoleData.ID);
        
        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant&&!PlayerData.My.MapRole[i].isNpc)
            {
                rolenongmin = PlayerData.My.MapRole[i];
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
       
        for (int i = 0; i <  role.warehouse.Count; i++)
        {
            if (role.warehouse[i].damage < 80)
            {
                role.warehouse.Remove(role.warehouse[i]);
            }
        }

        missiondatas.data[0].currentNum = role.warehouse.Count;
        if (role.warehouse.Count > missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            
        }

        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed )
            {
                missiondatas.data[1].isFinish = true;
                missiondatas.data[1].currentNum = 1;
                roleseed1 = PlayerData.My.MapRole[i];
            }
        }

        if (roleseed1 == null)
        {
          
        }
        else
        {
            if (TradeManager.My.CheckTwoRoleHasTrade(roleseed1.baseRoleData, rolenongmin.baseRoleData))
            {
                missiondatas.data[2].isFinish = true;
                missiondatas.data[2].currentNum = 1;
            }

        }

  
        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish && missiondatas.data[2].isFinish)
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }
}