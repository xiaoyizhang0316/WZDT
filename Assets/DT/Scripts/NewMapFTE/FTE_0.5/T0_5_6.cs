using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class T0_5_6 : BaseGuideStep
{
    private BaseMapRole peasantRole = null;
    private BaseMapRole seed1Role = null;
    private BaseMapRole seed2Role = null;

    public BaseMapRole dealer1;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        dealer1 = FTE_0_5Manager.My.dealerJC1.GetComponent<BaseMapRole>();
        FTE_0_5Manager.My.DownRole(FTE_0_5Manager.My.seerJC1);
        
        FTE_0_5Manager.My.UpRole(FTE_0_5Manager.My.dealerJC1);
        dealer1.warehouse.Clear();
        dealer1.OnMoved += ChangeColor;

        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed &&
                !PlayerData.My.MapRole[i].isNpc)
            {
                TradeManager.My.DeleteTrade(       PlayerData.My.MapRole[i].tradeList[0].tradeData.ID);
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
    
        missiondatas.data[0].currentNum = dealer1.warehouse.Count;
        if (dealer1.warehouse.Count >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
        }

        if ( missiondatas.data[0].isFinish)
        {
            return true;
        }

        return false;
    }
}