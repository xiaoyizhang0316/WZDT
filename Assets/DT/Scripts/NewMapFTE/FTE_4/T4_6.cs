using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class T4_6 : BaseGuideStep
{
    public  BaseGuideStep step;
    public override IEnumerator StepStart()
    {
        
     yield return     null;
    }

    public override IEnumerator StepEnd()
    {
        yield return     null;
         
    }

    public override bool ChenkEnd()
    {


        if (NewCanvasUI.My.Panel_TradeSetting.activeSelf&&CreateTradeManager.My.currentTrade.IsTradeSettingBest())
        {
            step.isOpen = false;
        
        }
        else if(NewCanvasUI.My.Panel_TradeSetting.activeSelf&&!CreateTradeManager.My.currentTrade.IsTradeSettingBest())
        {
            step.isOpen = true;

        }

        if (NewCanvasUI.My.Panel_TradeSetting.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }



    }
 
}
