using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Goal1 : BaseGuideStep
{
    public GameObject tabPanel;
    public int finalCost = 0;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0, 0.5f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        tabPanel.SetActive(true);
        GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().isBorn = false;
        CancelInvoke();
        yield return new WaitForSeconds(2);
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

        missiondatas.data[1].currentNum = StageGoal.My.totalCost*60/(StageGoal.My.timeCount==0?1:StageGoal.My.timeCount);
        if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
        {
            finalCost = missiondatas.data[1].currentNum;
            missiondatas.data[1].isFinish = true;
        }
    }
}
