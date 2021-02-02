using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using DG.Tweening;

public class FTE_1_5_Goal3_New : BaseGuideStep
{
    public int roleCostLimit;
    public int costLimit;
    public int timeLimit;
    public int needQuality;
    //public Transform QM;
    public GameObject costPanel;

    private int currentCost;
    private int currentTime;
    public override IEnumerator StepStart()
    {
        currentCost = StageGoal.My.productCost;
        currentTime = StageGoal.My.timeCount;
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        costPanel.GetComponent<CostPanel>().InitProductCost(currentCost, currentTime, 0);
        costPanel.GetComponent<CostPanel>().ShowProductCostAsMain(StageGoal.My.productCost-currentCost, timeLimit);
        NewCanvasUI.My.GamePause(false);
        FTE_1_5_Manager.My.qualityStation.SetActive(true);
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().checkQuality = needQuality;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().checkBuff = -1;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().needCheck = true;
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().QualityReset();
        
        SkipButton();
        InvokeRepeating("CheckGoal1",0, 0.2f);
        yield return null;
        FTE_1_5_Manager.My.UpRole(FTE_1_5_Manager.My.qualityStation, 0.32f);
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
        //InvokeRepeating("CheckGoal1",0, 0.2f);
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        currentCost = StageGoal.My.productCost;
        currentTime = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitProductCost(currentCost, currentTime, 0);
        tempCount = 0;
        lastCount = 0;
        costPanel.GetComponent<CostPanel>().ShowProductCostAsMain(StageGoal.My.productCost-currentCost, timeLimit);
        FTE_1_5_Manager.My.qualityStation.GetComponent<QualityRole>().QualityReset();
        missiondatas.data[1].isFail = false;
        missiondatas.data[1].currentNum = 0;
        InvokeRepeating("CheckGoal",0, 0.2f);
    }

    public int currentProductCost = 0;
    public int currentQuality = 0;
    private bool checkGoal = false;
    private bool checkHami = false;
    void CheckGoal1()
    {
        currentProductCost = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (!PlayerData.My.MapRole[i].baseRoleData.isNpc)
            {
                currentProductCost += PlayerData.My.MapRole[i].baseRoleData.cost;
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                {
                    if (currentQuality < PlayerData.My.MapRole[i].baseRoleData.effect*10)
                    {
                        currentQuality = PlayerData.My.MapRole[i].baseRoleData.effect*10;
                    }
                }
            }
        }

        missiondatas.data[0].currentNum = currentProductCost;
        if (currentQuality>=needQuality && currentProductCost<= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            if (!checkGoal)
            {
                CancelInvoke();
                CheckAllGoal();
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

    private void CheckAllGoal()
    {
        //InvokeRepeating("CheckGoal",0, 0.2f);
        InvokeRepeating("CheckGoal1",0, 0.2f);
    }

    private void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTime > timeLimit)
        {
            missiondatas.data[1].isFail = true;
            //NewCanvasUI.My.GamePause(false);
            HttpManager.My.ShowTip("超出时间限制，任务重置！");
            Reset();
            return;
        }
        
        costPanel.GetComponent<CostPanel>().ShowProductCostAsMain(StageGoal.My.productCost-currentCost, timeLimit);
        /*if (StageGoal.My.productCost - currentCost > costLimit)
        {
            missiondatas.data[1].isFail = true;
            //NewCanvasUI.My.GamePause(false);
            HttpManager.My.ShowTip("超出固定成本限制，任务重置！");
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

    private int tempCount = 0;
    private int lastCount = 0;

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
