using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FTE_1_5_Goal6 : BaseGuideStep
{
    private int currentIncome = 0;
    private int currentCost = 0;
    private float rate = 0;
    public override IEnumerator StepStart()
    {
        currentIncome = StageGoal.My.totalIncome;
        currentCost = StageGoal.My.totalCost;
        InvokeRepeating("CheckGoal",0, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            rate = (float)(StageGoal.My.totalIncome - currentIncome - StageGoal.My.totalCost + currentCost) /
                   (StageGoal.My.totalIncome - currentIncome);
            missiondatas.data[0].currentNum = (int)rate;
            if (rate >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }
}
