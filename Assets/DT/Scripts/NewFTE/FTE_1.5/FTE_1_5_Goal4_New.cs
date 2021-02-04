using System;
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
        costPanel.GetComponent<CostPanel>().InitTradeCost(currentCost, currentTime,0);
        costPanel.GetComponent<CostPanel>().ShowTradeCostAsMain(StageGoal.My.tradeCost-currentCost, timeLimit);
        NewCanvasUI.My.GamePause(false);
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().ClearWarehouse();
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().checkQuality = needQuality;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().checkBuff = -1;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().needCheck = true;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().QualityReset();
        SkipButton();
        InvokeRepeating("CheckGoal1",0, 0.2f);
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
        CancelInvoke("CheckGoal");
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        currentCost = StageGoal.My.tradeCost;
        currentTime = StageGoal.My.timeCount;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().QualityReset();
        lastCount = 0;
        tempCount = 0;
        costPanel.GetComponent<CostPanel>().InitTradeCost(currentCost, currentTime, 0);
        costPanel.GetComponent<CostPanel>().ShowTradeCostAsMain(StageGoal.My.tradeCost-currentCost, timeLimit);
        missiondatas.data[1].isFail = false;
        missiondatas.data[1].currentNum = 0;
        InvokeRepeating("CheckGoal",0, 0.2f);
    }

    private int costStat = 0;
    private int checkQuality = 0;
    private bool checkGoal = false;
    private bool checkHami = false;
    private int lastCount = 0;
    private int tempCount = 0;
    void CheckGoal1()
    {
        costStat = 0;
        checkQuality = 0;
        foreach (TradeSign sign in TradeManager.My.tradeList.Values)
        {
            costStat += sign.CalculateTC(true);
        }

        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                if (checkQuality < PlayerData.My.MapRole[i].baseRoleData.effect * 10)
                {
                    checkQuality = PlayerData.My.MapRole[i].baseRoleData.effect * 10;
                }
            }
        }

        missiondatas.data[0].currentNum = costStat;

        if (checkQuality >= needQuality && costStat <= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            if (!checkGoal)
            {
                CancelInvoke();
                Check();
                Reset();
                checkGoal = true;
            }

            if (!checkHami)
            {
                checkHami = true;
            }
        }
        else
        {
            missiondatas.data[0].isFinish = false;
            MissionManager.My.signs[0].ResetSuccess();
            if (checkGoal)
            {
                //CancelInvoke("CheckGoal");
                //checkGoal = false;
                checkHami = false;
                lastCount = missiondatas.data[1].currentNum;
            }
        }
    }

    void Check()
    {
        InvokeRepeating("CheckGoal1",0, 0.2f);
    }

    private void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTime > timeLimit)
        {
            missiondatas.data[1].isFail = true;
            HttpManager.My.ShowTip("超出时间限制，任务重置！");
            Reset();
            return;
        }
        
        costPanel.GetComponent<CostPanel>().ShowTradeCostAsMain(StageGoal.My.tradeCost-currentCost, timeLimit);
        /*if (StageGoal.My.tradeCost - currentCost > costLimit)
        {
            missiondatas.data[0].isFail = true;
            HttpManager.My.ShowTip("超出交易成本限制，任务重置！");
            Reset();
            return;
        }*/

        if (missiondatas.data[1].isFinish == false)
        {
            if (checkHami)
            {
                missiondatas.data[1].currentNum = lastCount +
                    FTE_1_5_Manager.My.qualityStation.GetComponent<BaseMapRole>().warehouse.Count - tempCount;
            }
            else
            {
                missiondatas.data[1].currentNum = lastCount;
                tempCount = FTE_1_5_Manager.My.qualityStation.GetComponent<BaseMapRole>().warehouse.Count;
            }
            //missiondatas.data[1].currentNum = FTE_1_5_Manager.My.qualityStation.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                NewCanvasUI.My.GamePause(false);
                missiondatas.data[1].isFinish = true;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[1].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        costPanel.GetComponent<CostPanel>().HideAllCost();
        yield return new WaitForSeconds(2f);
    }
}
