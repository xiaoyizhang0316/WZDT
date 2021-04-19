using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task10 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        StageGoal.My.killNumber = 0;
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
            missiondatas.data[0].currentNum = StageGoal.My.killNumber;
            if (missiondatas.data[0].CheckNumFinish())
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }


    public override bool ChenkEnd()
    {
        return missiondatas.CheckEnd();
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
