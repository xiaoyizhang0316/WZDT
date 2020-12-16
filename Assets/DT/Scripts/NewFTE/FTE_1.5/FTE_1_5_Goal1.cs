using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Goal1 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0, 1);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield break;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[1].isFinish;
    }

    void CheckGoal()
    {
        missiondatas.data[0].currentNum = StageGoal.My.killNumber;
        if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
        }

        missiondatas.data[1].currentNum = StageGoal.My.totalCost;
        if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
        {
            missiondatas.data[1].isFinish = true;
        }
    }
}
