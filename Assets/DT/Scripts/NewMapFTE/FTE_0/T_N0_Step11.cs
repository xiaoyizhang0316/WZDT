using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N0_Step11 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.GamePause(false);
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        NewCanvasUI.My.GameNormal();
        yield return new WaitForSeconds(0.5f);
    }
}
