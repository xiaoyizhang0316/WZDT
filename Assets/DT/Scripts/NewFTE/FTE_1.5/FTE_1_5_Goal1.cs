using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Goal1 : BaseGuideStep
{
    public GameObject tabPanel;
    public int finalCost = 0;
    public GameObject costPanel;

    public int limitTime;
    private int currentTime = 0;
    private bool fail;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0, 0.5f);
        currentTime = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(0,currentTime);
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
        return fail;
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTime >= limitTime)
        {
            fail = true;
            missiondatas.data[0].isFinish = false;
            missiondatas.data[1].isFinish = false;
            return;
        }
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = StageGoal.My.killNumber;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
        
        missiondatas.data[1].currentNum = StageGoal.My.totalCost;
        costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum,limitTime);
        if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
        {
            finalCost = missiondatas.data[1].currentNum;
            FTE_1_5_Manager.My.goal1FinalCost = finalCost;
            missiondatas.data[1].isFinish = false;
            fail = true;
        }
        else
        {
            missiondatas.data[1].isFinish = true;
        }
    }
}
