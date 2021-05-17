using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_5 : BaseGuideStep
{
    public GameObject isTradePanel;

    public IsPointOn kuang;
    public override IEnumerator StepStart()
    {
        
     yield return   new WaitForSeconds(1);
    }

    public override IEnumerator StepEnd()
    {
     
        yield return   new WaitForSeconds(1);
         
    }

    public override bool ChenkEnd()
    {

        if (NewCanvasUI.My.Panel_TradeSetting.activeSelf)
        {
            kuang.gameObject.SetActive(true);
        }

        else
        {
            kuang.gameObject.SetActive(false);
            
        }
        for (int i = 0; i <missiondatas.data.Count; i++)
        {
            if(kuang.IsOn )
                missiondatas.data[i].isFinish = true;
        }
        return kuang.IsOn;
    }
 
}
