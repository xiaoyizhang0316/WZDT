using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FTE_1_5_Goal5 : BaseGuideStep
{
    public GameObject fruitQT;
    private int currentIncome = 0;
    public override IEnumerator StepStart()
    {
        fruitQT.SetActive(false);
        currentIncome = StageGoal.My.totalIncome;
        //StageGoal.My.totalIncome = 0;
        NewGuideManager.My.BornEnemy1();
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
            missiondatas.data[0].currentNum = StageGoal.My.totalIncome-currentIncome;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }
}
