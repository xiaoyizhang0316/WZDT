using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N0_Step9 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return StageGoal.My.isTurnStart;
    }

    public override IEnumerator StepEnd()
    {
        T_N0_Manager.My.isTasteKill = false;
        yield return null;
    }
}
