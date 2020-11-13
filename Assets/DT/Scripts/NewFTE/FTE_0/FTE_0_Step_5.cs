using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_0_Step_5 : BaseGuideStep
{
    public Transform border;
    bool next = false;
    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        next = false;
        yield return new WaitForSeconds(15);
        next = true;
    }

    public override bool ChenkEnd()
    {
        if (next)
        {
            NewCanvasUI.My.GamePause(false);
            return true;
        }
        return false;
    }
}
