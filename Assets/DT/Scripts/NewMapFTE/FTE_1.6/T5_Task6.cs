using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5_Task6 : BaseGuideStep
{
    private int startCost = 0;
    private int startIncome = 0;
    public override IEnumerator StepStart()
    {
        T5_Manager.My.leftKillNum = 0;
        T5_Manager.My.rightKillNum = 0;
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
        if (!missiondatas.data[0].isFinish)
        {
            missiondatas.data[0].currentNum = T5_Manager.My.leftKillNum;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
        
        if (!missiondatas.data[1].isFinish)
        {
            missiondatas.data[1].currentNum = T5_Manager.My.rightKillNum;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }

        if (!missiondatas.data[2].isFinish)
        {
            missiondatas.data[2].currentNum =
                StageGoal.My.totalIncome - startIncome - StageGoal.My.totalCost + startCost;
            if (missiondatas.data[2].currentNum <= -12000)
            {
                startCost = StageGoal.My.totalCost;
                startIncome = StageGoal.My.totalIncome;
                missiondatas.data[2].currentNum =
                    StageGoal.My.totalIncome - startIncome - StageGoal.My.totalCost + startCost;
            }
            if (missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum)
            {
                missiondatas.data[2].isFinish = true;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&& missiondatas.data[1].isFinish && missiondatas.data[2].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
