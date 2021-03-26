using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_10_6 : BaseGuideStep
{

    public BaseMapRole nong;
   
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        FTE_0_5Manager.My.clearWarehouse = 1;
        yield return null;
    }

    public override IEnumerator StepEnd()
    {

        yield return new WaitForSeconds(2);
    }
 

    public override bool ChenkEnd()
    {
        if (FTE_0_5Manager.My.clearWarehouse == 2)
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
