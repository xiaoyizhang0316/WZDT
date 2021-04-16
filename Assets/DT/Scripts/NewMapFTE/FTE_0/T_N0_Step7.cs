using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N0_Step7 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return FloatWindow.My.GetComponent<RectTransform>().anchoredPosition.x < 9000;
    }

    public override IEnumerator StepEnd()
    {
        yield return null;
    }
}
