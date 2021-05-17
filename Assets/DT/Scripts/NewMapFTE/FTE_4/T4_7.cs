using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_7 : BaseGuideStep
{
 
    public GameObject bordxian;
    public GameObject bordhou;
    public override IEnumerator StepStart()
    {
        bordxian.SetActive(false);
        bordhou.SetActive(false);
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield return null;

         
    }

    public override bool ChenkEnd()
    {
        if (NewCanvasUI.My.Panel_TradeSetting.activeSelf)
        {
            if (CreateTradeManager.My.currentTrade.tradeData.selectCashFlow == GameEnum.CashFlowType.先钱)
            {
                bordhou.SetActive(true);
            }
            
            if (CreateTradeManager.My.currentTrade.tradeData.selectCashFlow == GameEnum.CashFlowType.后钱)
            {
                bordxian.SetActive(true);
            }
        }

        else
        {
            bordxian.SetActive(false);
            bordhou.SetActive(false);
        }

        if (CreateTradeManager.My.currentTrade.IsTradeSettingBest())
        {
            for (int i = 0; i <missiondatas.data.Count; i++)
            { 
                    missiondatas.data[i].isFinish = true;
            }
            return true;

        }
        else
        {
            return false;
        }
    }
 
}
