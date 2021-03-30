using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3_4 : BaseGuideStep
{
 
   
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        FTE_0_6Manager.My.SetClearWHButton(true);
        FTE_0_6Manager.My.clearWarehouse = 1;
        yield return null;
    }

    public override IEnumerator StepEnd()
    {

        yield return new WaitForSeconds(2);
    }
 

    public override bool ChenkEnd()
    {
        if (FTE_0_6Manager.My.clearWarehouse == 2)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }

        return false;
    }
}
