using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class T4_Task10 : BaseGuideStep
{
    private int startCost = 0;
    private int startIncome = 0;

    public BornType bornType;
    
    public override IEnumerator StepStart()
    {
        startCost = StageGoal.My.totalCost;
        startIncome = StageGoal.My.totalIncome;
        T4_Manager.My.BornConsumer((int) bornType.type, bornType.count);
        InvokeRepeating("CheckConsumer", 4, 0.5f);
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    private bool isNoConsumer = false;
    void CheckConsumer()
    {
        if (!T4_Manager.My.CheckHasConsume(T4_Manager.My.bornPoint.transform))
        {
            isNoConsumer = true;
            CancelInvoke("CheckConsumer");
        }
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

            if (isNoConsumer)
            {
                isNoConsumer = false;
                startCost = StageGoal.My.totalCost;
                startIncome = StageGoal.My.totalIncome;
                T4_Manager.My.BornConsumer((int) bornType.type, bornType.count);
                InvokeRepeating("CheckConsumer", 4, 0.5f);
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
