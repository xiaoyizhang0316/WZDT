using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FTE_1_5_Goal7 : BaseGuideStep
{
    private int currentTimeCount = 0;
    private int currentCost = 0;
    public FTE_1_5_Goal1 goal1;
    public override IEnumerator StepStart()
    {
        currentTimeCount = StageGoal.My.totalIncome;
        currentCost = StageGoal.My.totalCost;
        StageGoal.My.killNumber = 0;
        //missiondatas.data[1].content += "<color=red>\n上次的周期成本是" + goal1.finalCost + "</color>";
        MissionData missionData = new MissionData();
        missionData.content="<color=red>\n上次的周期成本是" + goal1.finalCost + "</color>";
        missionData.isFinish = true;
        MissionManager.My.AddMission(missionData);
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
        return missiondatas.data[0].isFinish && missiondatas.data[1].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = StageGoal.My.killNumber;
            missiondatas.data[1].currentNum = (StageGoal.My.totalCost - currentCost) * 60 /
                                              (StageGoal.My.timeCount - currentTimeCount);
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = (StageGoal.My.totalCost - currentCost) * 60 /
                                              (StageGoal.My.timeCount - currentTimeCount);
            if (missiondatas.data[1].currentNum <= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }
    }
}
