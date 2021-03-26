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
    public GameObject openCG;
    public GameObject bornPoint;
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.GamePause(false);
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        StageGoal.My.killNumber = 0;
        //missiondatas.data[1].content += "<color=red>\n上次的周期成本是" + goal1.finalCost + "</color>";
        MissionData missionData = new MissionData();
        missionData.content="上次的周期成本是<color=red>" + FTE_1_5_Manager.My.goal1FinalCost + "</color>";
        missionData.isFail = true;
        MissionManager.My.AddMission(missionData, missionTitle);
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTimeCount,costLimit);
        InvokeRepeating("CheckGoal",0, 0.2f);
        //StartCoroutine( bornPoint.GetComponent<Building>().BornEnemy1(25));
        NewGuideManager.My.BornEnemy1(25);
        //Reset();
        SkipButton();
        //InvokeRepeating("CheckGoal",0, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }
  
    void SkipButton()
    {
        if (needCheck && FTE_1_5_Manager.My.needSkip)
        {
            if (endButton != null)
            {
                
                endButton.onClick.AddListener(() =>
                {
                    for (int i = 0; i < missiondatas.data.Count; i++)
                    {
                        missiondatas.data[i].isFinish = true;
                    }
                });
                endButton.interactable = true;
                endButton.gameObject.SetActive(true);
            }
        }
    }
    

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        //NewCanvasUI.My.GamePause(false);
        bornPoint.GetComponent<Building>().isBorn = false;
        yield return new WaitForSeconds(2f);
        //costPanel.GetComponent<CostPanel>().HideAllCost();
        openCG.SetActive(true);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if ( PlayerData.My.totalRoleCount == 0 && StageGoal.My.playerTechPoint<10)
        {
            StageGoal.My.playerTechPoint = 10;
            StageGoal.My.GetTechPoint(0);
        }
        
        if (StageGoal.My.timeCount - currentTimeCount >= limitTime)
        {
            HttpManager.My.ShowTip("超出时间限制，任务重置！");
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            Reset();
            return;
        }
        costPanel.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost-currentCost,limitTime);
        if (StageGoal.My.totalCost - currentCost >= costLimit)
        {
            HttpManager.My.ShowTip("超出成本限制，任务重置！");
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
        TradeManager.My.ResetAllTrade();
        //FTE_1_5_Manager.My.isClearGoods=true;
        PlayerData.My.ClearAllRoleWarehouse();
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTimeCount,costLimit);
        InvokeRepeating("CheckGoal",0, 0.2f);
        //FTE_1_5_Manager.My.isClearGoods=false;
        NewCanvasUI.My.GameNormal();
    }
}
