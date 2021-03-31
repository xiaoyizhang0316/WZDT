using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class T4_Task11 : BaseGuideStep
{
    private int startCost = 0;
    private int startIncome = 0;
    public int timeInterval = 0;

    public List<BornType> bornTypes;
    
    public override IEnumerator StepStart()
    {
        startCost = StageGoal.My.totalCost;
        startIncome = StageGoal.My.totalIncome;
        StartCoroutine(BornConsumer());
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

            if (!T4_Manager.My.CheckHasConsume(T4_Manager.My.bornPoint.transform)&& bornEnd)
            {
                startCost = StageGoal.My.totalCost;
                startIncome = StageGoal.My.totalIncome;
                StartCoroutine(BornConsumer());
            }
        }
    }

    private bool bornEnd = false;
    IEnumerator BornConsumer()
    {
        bornEnd = false;
        T4_Manager.My.BornConsumer((int) bornTypes[0].type,bornTypes[0].count);
        yield return new WaitForSeconds(timeInterval);
        T4_Manager.My.BornConsumer((int) bornTypes[1].type,bornTypes[1].count);
        yield return new WaitForSeconds(timeInterval);
        T4_Manager.My.BornConsumer((int) bornTypes[2].type,bornTypes[2].count);
        yield return new WaitForSeconds(2);
        bornEnd = true;
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
