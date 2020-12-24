using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Goal1 : BaseGuideStep
{
    public GameObject tabPanel;
    public int finalCost = 0;
    public GameObject costPanel;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0, 0.5f);
        costPanel.GetComponent<CostPanel>().InitCostPanel(0,0);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        tabPanel.SetActive(true);
        GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().isBorn = false;
        CancelInvoke();
        yield return new WaitForSeconds(2);
        costPanel.GetComponent<CostPanel>().HideAllCost();
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[1].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = StageGoal.My.killNumber;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        if (missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = StageGoal.My.totalCost*60/(StageGoal.My.timeCount==0?1:StageGoal.My.timeCount);
            costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum);
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                finalCost = missiondatas.data[1].currentNum;
                missiondatas.data[1].isFinish = true;
            }
        }
    }
}
