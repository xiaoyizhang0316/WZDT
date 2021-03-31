using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T7_Task3 : BaseGuideStep
{
    public GameObject tradeManager;
    public GameObject red;
    public GameObject red1;
    private TradeSign currentTS;
    public override IEnumerator StepStart()
    {
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            if (tradeManager.activeInHierarchy)
            {
                currentTS = tradeManager.GetComponent<CreateTradeManager>().currentTrade;

                if (CheckTrade(currentTS))
                {
                    red.SetActive(true);
                    if (tradeManager.GetComponent<CreateTradeManager>().selectCashFlow == GameEnum.CashFlowType.后钱)
                    {
                        red.SetActive(false);
                        red1.SetActive(true);
                    }
                    else
                    {
                        red.SetActive(true);
                        red1.SetActive(false);
                    }
                }
                else
                {
                    currentTS = null;
                }
            }
            else
            {
                red.SetActive(false);
                red1.SetActive(false);
                if (currentTS != null)
                {
                    if (currentTS.tradeData.selectCashFlow == GameEnum.CashFlowType.后钱)
                    {
                        missiondatas.data[0].isFinish = true;
                    }
                }
            }
        }
    }

    bool CheckTrade(TradeSign tradeSign)
    {
        bool castIsSeed =
            PlayerData.My.GetMapRoleById(double.Parse(tradeSign.tradeData.castRole)).baseRoleData.baseRoleData
                .roleType == GameEnum.RoleType.Seed;
        
        bool targetIsPeasant =
            PlayerData.My.GetMapRoleById(double.Parse(tradeSign.tradeData.targetRole)).baseRoleData.baseRoleData
                .roleType == GameEnum.RoleType.Peasant;

        return castIsSeed && targetIsPeasant;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish ;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
