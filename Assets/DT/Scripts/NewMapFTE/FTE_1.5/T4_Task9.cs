using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class T4_Task9 : BaseGuideStep
{
    private int startCost = 0;
    private int startIncome = 0;
    
    public override IEnumerator StepStart()
    {
        startCost = StageGoal.My.totalCost;
        startIncome = StageGoal.My.totalIncome;
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    void CheckGoal()
    {
        
        if (!missiondatas.data[0].isFinish )
        {
            missiondatas.data[0].currentNum = StageGoal.My.totalIncome - StageGoal.My.totalCost + startCost - startIncome;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }

            if (missiondatas.data[0].currentNum < -12000)
            {
                startCost = StageGoal.My.totalCost;
                startIncome = StageGoal.My.totalIncome;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
