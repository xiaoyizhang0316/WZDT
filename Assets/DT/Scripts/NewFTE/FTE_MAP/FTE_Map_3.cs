using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_Map_3 : BaseGuideStep
{
    public GameObject openTargetPanel;

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
        if (openTargetPanel.activeInHierarchy)
        {
            return true;
        }
        return false;
    }
}
