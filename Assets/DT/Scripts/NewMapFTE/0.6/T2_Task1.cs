using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task1 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        // TODO 生成消费者
        if (StageGoal.My.killNumber != 0)
        {
            StageGoal.My.killNumber = 0;
        }
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
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
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
