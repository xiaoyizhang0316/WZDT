using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_Map_2 : BaseGuideStep
{
    public override IEnumerator StepEnd()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator StepStart()
    {
        throw new System.NotImplementedException();
    }

    public override bool ChenkEnd()
    {
        if (LevelInfoManager.My.stepOver)
        {
            NetworkMgr.My.UpdateUnlockStatus("1_1_0_0_0_0_0_0_0");
        }
        return false;
    }
}
