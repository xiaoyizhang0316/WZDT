using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class T4_Task10 : BaseGuideStep
{
    private int startCost = 0;
    private int startIncome = 0;
    
    public override IEnumerator StepStart()
    {
        startCost = StageGoal.My.totalCost;
        startIncome = StageGoal.My.totalIncome;
        T4_Manager.My.BornConsumer(0);
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
            missiondatas.data[0].currentNum =
                StageGoal.My.totalIncome - StageGoal.My.totalCost - startIncome + startCost;

            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }

            if (!T4_Manager.My.CheckHasConsume(T4_Manager.My.bornPoint.transform))
            {
                startCost = StageGoal.My.totalCost;
                startIncome = StageGoal.My.totalIncome;
                T4_Manager.My.BornConsumer(0);
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
