using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Goal1 : BaseGuideStep
{
    public GameObject tabPanel;
    public int costLimit;
    public int finalCost = 0;
    public GameObject costPanel;

    public int limitTime;
    private int currentTime = 0;
    private bool fail;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0, 0.5f);
        currentTime = StageGoal.My.timeCount;
        NewCanvasUI.My.GamePause(false);
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
        return missiondatas.data[0].isFail;
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTime >= limitTime)
        {
            //fail = true;
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            FTE_1_5_Manager.My.goal1FinalCost = StageGoal.My.totalCost;
            return;
        }
        costPanel.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost,limitTime);
        if (StageGoal.My.totalCost >= costLimit)
        {
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            FTE_1_5_Manager.My.goal1FinalCost = StageGoal.My.totalCost;
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
    }
}
