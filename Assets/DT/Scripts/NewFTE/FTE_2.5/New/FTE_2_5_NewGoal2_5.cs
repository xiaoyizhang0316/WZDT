using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_NewGoal2_5 : BaseGuideStep
{
    public GameObject tradeManager;
    public GameObject red;
    public GameObject red1;
    private TradeSign currentTS;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.05f);
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            if (tradeManager.activeInHierarchy)
            {
                currentTS = tradeManager.GetComponent<CreateTradeManager>().currentTrade;
                
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

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(2);
    }
}
