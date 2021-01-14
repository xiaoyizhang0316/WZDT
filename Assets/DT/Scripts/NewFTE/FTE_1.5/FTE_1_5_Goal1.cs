using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Goal1 : BaseGuideStep
{
    public GameObject tabPanel;
    public int costLimit;
    //public int finalCost = 0;
    public int limitTime;
    public GameObject costPanel;

    private int currentTime = 0;
    private int currentCost = 0;
    private bool fail;
    public GameObject bornPoint;
    public GameObject rectBoard;
    public override IEnumerator StepStart()
    {
        currentTime = StageGoal.My.timeCount;
        currentCost = StageGoal.My.totalCost;
        NewCanvasUI.My.GamePause(false);
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTime, 0);
        InvokeRepeating("CheckGoal", 0, 0.5f);
        SkipButton();
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        //tabPanel.SetActive(true);
        CancelInvoke();

        yield return new WaitForSeconds(5);
        bornPoint.GetComponent<Building>().isBorn = false;
        costPanel.GetComponent<CostPanel>().HideAllCost();
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
                        missiondatas.data[i].isFail = true;
                    }

                    isEnd = true;

                });
                endButton.interactable = true;
                endButton.gameObject.SetActive(true);
            }
        }
    }

    private bool isEnd = false;
    public override bool ChenkEnd()
    {
        //return missiondatas.data[0].isFail;
        if (isEnd)
        {
            FTE_1_5_Manager.My.goal1FinalCost = StageGoal.My.totalCost-currentCost;
        }
        return isEnd;
    }

    private void Reset()
    {
        CancelInvoke();
        missiondatas.data[0].isFail = false;
        missiondatas.data[0].currentNum = 0;
        StageGoal.My.killNumber = 0;
        currentTime = StageGoal.My.timeCount;
        currentCost = StageGoal.My.totalCost;
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTime, 0);
        InvokeRepeating("CheckGoal", 0, 0.5f);
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTime >= limitTime)
        {
            //fail = true;
            HttpManager.My.ShowTip("超出时间限制，重置任务！");
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            //FTE_1_5_Manager.My.goal1FinalCost = StageGoal.My.totalCost;
            Reset();
            return;
        }
        costPanel.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost-currentCost,limitTime);
        
        if (missiondatas.data[0].isFinish == false)
        {                
            missiondatas.data[0].currentNum = StageGoal.My.killNumber;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
                MissionData missionData = new MissionData();
                missionData.content = "成本不超过<color=#FF6D09>"+costLimit+"</color>";
                missionData.currentNum = StageGoal.My.totalCost - currentCost;
                missionData.maxNum = costLimit;
                missionData.isMainmission = false;
                missionData.isFail = true;
                MissionManager.My.AddMission(missionData);
                rectBoard.SetActive(true);
                if (StageGoal.My.totalCost - currentCost > costLimit)
                {
                    HttpManager.My.ShowTip("超出成本限制，本次任务失败！");
                }
                isEnd = true;
            }
        }
    }
}
