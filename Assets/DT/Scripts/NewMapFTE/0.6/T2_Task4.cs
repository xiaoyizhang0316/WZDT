using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task4 : BaseGuideStep
{
    public GameObject redTipBuild;
    public override IEnumerator StepStart()
    {
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    void CheckGoal()
    {
        if (!missiondatas.data[0].isFinish)
        {
            redTipBuild.SetActive(true);
            if (PlayerData.My.seedCount >= 2)
            {
                redTipBuild.SetActive(false);
                missiondatas.data[0].isFinish = true;
            }
        }
    }


    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(3);
    }
}
