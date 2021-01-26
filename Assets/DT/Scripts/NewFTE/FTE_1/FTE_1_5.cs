using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5 : BaseGuideStep
{
    public GameObject border;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.5f);
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish ;
    }

    void CheckGoal()
    {
        if (!border.activeInHierarchy)
        {
            missiondatas.data[0].isFinish = true;
        }
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2);
        if (missionTitle.StartsWith("六"))
        {
            // do end
        }
    }
}
