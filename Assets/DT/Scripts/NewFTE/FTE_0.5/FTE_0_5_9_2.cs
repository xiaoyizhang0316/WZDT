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
        dealer1.OnMoved += ChangeColor;
        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant &&
                !PlayerData.My.MapRole[i].isNpc)
            {
                peasantRole = PlayerData.My.MapRole[i];
            }
        }
        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed &&
                !PlayerData.My.MapRole[i].isNpc)
            {
                PlayerData.My.MapRole[i].tradeButton.SetActive(true);
            }
        }
        yield return null;
    }

    public override IEnumerator StepEnd()
    {


        yield return new WaitForSeconds(2f);
    }

    public void ChangeColor(ProductData data)
    {
        if (data.damage >= 0)
        {
            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.dealerJC1_ran, FTE_0_5Manager.My.sg);
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.dealerJC1_ran, FTE_0_5Manager.My.sr);
        }
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

            return false;
        }
        if (seed1Role == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed &&
                    !PlayerData.My.MapRole[i].isNpc)
                {
                    seed1Role = PlayerData.My.MapRole[i];
                }
            }
            return false;

        }
        if (seed2Role == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed &&
                    !PlayerData.My.MapRole[i].isNpc&&PlayerData.My.MapRole[i]!=seed1Role)
                {
                    seed2Role = PlayerData.My.MapRole[i];
                }
            }
            return false;

        }

        if (TradeManager.My.CheckTwoRoleHasTrade(seed1Role.baseRoleData, peasantRole.baseRoleData)||TradeManager.My.CheckTwoRoleHasTrade(seed2Role.baseRoleData, peasantRole.baseRoleData))
        {
            missiondatas.data[0].isFinish = true;
             
        }
        else
        {
            missiondatas.data[0].isFinish = false;

        }
        missiondatas.data[1].currentNum = dealer1.warehouse.Count;
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