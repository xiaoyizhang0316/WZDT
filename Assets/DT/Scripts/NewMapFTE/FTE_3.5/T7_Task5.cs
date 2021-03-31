using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T7_Task5 : BaseGuideStep
{
    private int startCost = 0;
    private int startIncome = 0;
    public override IEnumerator StepStart()
    {
        T7_Manager.My.leftKillNum = 0;
        T7_Manager.My.rightKillNum = 0;
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = T7_Manager.My.leftKillNum;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        if (missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = T7_Manager.My.rightKillNum;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }

        if (missiondatas.data[2].isFinish == false)
        {
            missiondatas.data[2].currentNum = GetCurrentIncome();
            if (missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum)
            {
                missiondatas.data[2].isFinish = true;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish && missiondatas.data[1].isFinish && missiondatas.data[2].isFinish ;
    }

    int GetCurrentIncome()
    {
        return StageGoal.My.totalIncome - StageGoal.My.totalCost - startIncome + startCost;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
