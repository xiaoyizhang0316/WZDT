using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_10_3 : BaseGuideStep
{

    public BaseMapRole dealer1;
    public BaseMapRole dealer2;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
       
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2);
    }
 

    public override bool ChenkEnd()
    {
        if (dealer1.startTradeList.Count == 0 && dealer1.endTradeList.Count == 0&&
            dealer2.startTradeList.Count == 0 && dealer2.endTradeList.Count == 0
            )
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }
        
        return false;
    }
}

// if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
     // {
     //     return true;
     // }
     // else
     // {
     //     return false;
     // } 
