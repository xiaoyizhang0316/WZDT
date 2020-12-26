using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FTE_1_5_Goal7 : BaseGuideStep
{
    public int costLimit;
    public int limitTime = 0;
    private int currentTimeCount = 0;
    private int currentCost = 0;
    //public FTE_1_5_Goal1 goal1;
    public GameObject costPanel;
    public override IEnumerator StepStart()
    {
        StageGoal.My.killNumber = 0;
        //missiondatas.data[1].content += "<color=red>\n上次的周期成本是" + goal1.finalCost + "</color>";
        MissionData missionData = new MissionData();
        missionData.content="<color=red>\n上次的周期成本是" + FTE_1_5_Manager.My.goal1FinalCost + "</color>";
        missionData.isFinish = true;
        MissionManager.My.AddMission(missionData);
        Reset();
        //InvokeRepeating("CheckGoal",0, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(2f);
        costPanel.GetComponent<CostPanel>().HideAllCost();
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish && missiondatas.data[1].isFinish;
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTimeCount >= limitTime)
        {
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            Reset();
            return;
        }
        costPanel.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost-currentCost,limitTime);
        if (StageGoal.My.totalCost - currentCost >= costLimit)
        {
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            Reset();
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

        
            /*missiondatas.data[1].currentNum = (StageGoal.My.totalCost - currentCost) * 60 /
                                              ((StageGoal.My.timeCount - currentTimeCount)==0?1:(StageGoal.My.timeCount - currentTimeCount));
            costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum,limitTime);
            if (missiondatas.data[1].currentNum <= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
            else
            {
                missiondatas.data[0].isFinish = false;
                missiondatas.data[1].isFinish = false;
                Reset();
            }*/
        
    }
    
    void Reset()
    {
        CancelInvoke();
        NewCanvasUI.My.GamePause(false);
        StageGoal.My.killNumber = 0;
        missiondatas.data[0].isFail = false;
        FTE_1_5_Manager.My.isClearGoods=true;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].ClearWarehouse();
        }
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTimeCount);
        InvokeRepeating("CheckGoal",0, 0.2f);
        FTE_1_5_Manager.My.isClearGoods=false;
        NewCanvasUI.My.GameNormal();
    }
}
