using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class T0_5_7 : BaseGuideStep
{

    public GameObject nongminLock;
    private BaseMapRole seedRole = null;
    private BaseMapRole nong1Role = null;
    private BaseMapRole nong2Role = null;
    public GameObject red;
    // Update is called once per frame
    public override IEnumerator StepStart()
    { 
        red.SetActive(true);
        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed &&
                !PlayerData.My.MapRole[i].isNpc)
            {
                seedRole = PlayerData.My.MapRole[i];
            }
        }
        nongminLock.SetActive(false);
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        red.SetActive(false);

        yield return new WaitForSeconds(2f);

    }


    public override bool ChenkEnd()
    {
        if (seedRole == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed &&
                    !PlayerData.My.MapRole[i].isNpc)
                {
                    seedRole = PlayerData.My.MapRole[i];
                }
            }

            return false;
        }
        if (nong1Role == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType. Peasant&&
                    !PlayerData.My.MapRole[i].isNpc)
                {
                    nong1Role = PlayerData.My.MapRole[i];
                }
            }
            return false;

        }
        if (nong2Role == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant &&
                    !PlayerData.My.MapRole[i].isNpc&&PlayerData.My.MapRole[i]!=nong1Role)
                {
                    nong2Role = PlayerData.My.MapRole[i];
                }
            }
            return false;

        }

        if (TradeManager.My.CheckTwoRoleHasTrade(seedRole.baseRoleData, nong1Role.baseRoleData)&&TradeManager.My.CheckTwoRoleHasTrade(seedRole.baseRoleData, nong2Role.baseRoleData))
        {
            missiondatas.data[1].isFinish = true;
            red.SetActive(false);
        }
        else
        {
            missiondatas.data[1].isFinish = false;

        }
        
        TradeManager.My.ShowAllIcon(); 
        int count = 0;
        count = PlayerData.My.peasantCount;
        missiondatas.data[0].currentNum = count;
        if (count >= GetComponent<UnlockRoleFTE>().peasant)
        {
            missiondatas.data[0].isFinish = true;
            missiondatas.data[0].currentNum = 1;
           

        }
        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            return true;
        }

        return false;
    }
}