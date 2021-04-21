using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N0_Step10 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        T_N0_Manager.My.isTasteKill = false;
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return T_N0_Manager.My.isTasteKill;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
