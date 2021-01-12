using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_9_2 : BaseGuideStep
{
    private BaseMapRole peasantRole = null;
    private BaseMapRole seed1Role = null;
    private BaseMapRole seed2Role = null;

    public BaseMapRole dealer1;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        dealer1.warehouse.Clear();
        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant &&
                !PlayerData.My.MapRole[i].isNpc)
            {
                peasantRole = PlayerData.My.MapRole[i];
            }
        }
        yield return null;
    }

    public override IEnumerator StepEnd()
    {


        yield return new WaitForSeconds(1f);
    }


    public override bool ChenkEnd()
    {
        if (peasantRole == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant &&
                    !PlayerData.My.MapRole[i].isNpc)
                {
                    peasantRole = PlayerData.My.MapRole[i];
                }
            }
        }
        if (peasantRole == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed &&
                    !PlayerData.My.MapRole[i].isNpc)
                {
                    seed1Role = PlayerData.My.MapRole[i];
                }
            }
        }
        if (peasantRole == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant &&
                    !PlayerData.My.MapRole[i].isNpc&&PlayerData.My.MapRole[i]!=seed1Role)
                {
                    seed2Role = PlayerData.My.MapRole[i];
                }
            }
        }

        if (TradeManager.My.CheckTwoRoleHasTrade(peasantRole.baseRoleData, seed1Role.baseRoleData)||TradeManager.My.CheckTwoRoleHasTrade(peasantRole.baseRoleData, seed2Role.baseRoleData))
        {
            missiondatas.data[0].isFinish = true;
             
        }
        else
        {
            missiondatas.data[0].isFinish = false;

        }

        if (dealer1.warehouse.Count >= missiondatas.data[1].maxNum)
        {
            missiondatas.data[1].isFinish = true;
        }

        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            return true;
        }

        return false;
    }
}