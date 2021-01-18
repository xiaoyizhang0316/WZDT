﻿using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using DG.Tweening;

public class FTE_1_5_Goal4_New : BaseGuideStep
{
    public int costLimit;
    public int timeLimit;
    public int needQuality;
    //public Transform QM;
    public GameObject costPanel;

    private int currentCost;
    private int currentTime;
    public override IEnumerator StepStart()
    {
        currentCost = StageGoal.My.tradeCost;
        currentTime = StageGoal.My.timeCount;
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        costPanel.GetComponent<CostPanel>().InitTradeCost(currentCost, currentTime,costLimit);
        NewCanvasUI.My.GamePause(false);
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().ClearWarehouse();
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().checkQuality = needQuality;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().checkBuff = -1;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().needCheck = true;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().QualityReset();
        SkipButton();
        InvokeRepeating("CheckGoal",0, 0.2f);
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

    private void Reset()
    {
        CancelInvoke();
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        currentCost = StageGoal.My.tradeCost;
        currentTime = StageGoal.My.timeCount;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().QualityReset();
        costPanel.GetComponent<CostPanel>().InitTradeCost(currentCost, currentTime, costLimit);
        missiondatas.data[0].isFail = false;
        missiondatas.data[0].currentNum = 0;
        InvokeRepeating("CheckGoal",0, 0.2f);
    }

    private void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTime > timeLimit)
        {
            missiondatas.data[0].isFail = true;
            HttpManager.My.ShowTip("超出时间限制，任务重置！");
            Reset();
            return;
        }
        
        costPanel.GetComponent<CostPanel>().ShowTradeCostAsMain(StageGoal.My.tradeCost-currentCost, timeLimit);
        if (StageGoal.My.tradeCost - currentCost > costLimit)
        {
            missiondatas.data[0].isFail = true;
            HttpManager.My.ShowTip("超出交易成本限制，任务重置！");
            Reset();
            return;
        }

        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = FTE_1_5_Manager.My.qualityStation.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                NewCanvasUI.My.GamePause(false);
                missiondatas.data[0].isFinish = true;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        costPanel.GetComponent<CostPanel>().HideAllCost();
        yield return new WaitForSeconds(2f);
    }
}
