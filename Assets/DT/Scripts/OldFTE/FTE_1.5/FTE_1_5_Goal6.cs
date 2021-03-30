﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fungus;
using UnityEngine;

public class FTE_1_5_Goal6 : BaseGuideStep
{
    private int currentIncome = 0;
    private int currentCost = 0;
    private int currentTimeCount = 0;
    private int profit = 0;
    private int income=0;

   // public GameObject seed;
    public GameObject seedPlace;
   // public GameObject merchant;
    public GameObject merchantPlace;
    public int limitTime;
    public GameObject costPanel;
    public GameObject bornPoint;
    
    public override IEnumerator StepStart()
    {
        StartCoroutine( bornPoint.GetComponent<Building>().BornEnemy1(30));
        NewCanvasUI.My.GamePause(false);
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        FTE_1_5_Manager.My.npcSeed.SetActive(true);
        FTE_1_5_Manager.My.npcPeasant.SetActive(true);
        
        /*seed.transform.DOLocalMoveY(0.32f, 0.5f).Play().OnPause(() =>
        {
            seed.transform.DOLocalMoveY(0.32f, 0.5f).Play();
        });
        seedPlace.transform.DOLocalMoveY(0, 0.5f).Play().OnPause(() =>
        {
            seedPlace.transform.DOLocalMoveY(0, 0.5f).Play();
        });
        merchant.transform.DOLocalMoveY(0.32f, 0.5f).Play().OnPause(() =>
        {
            merchant.transform.DOLocalMoveY(0.32f, 0.5f).Play();
        });
        merchantPlace.transform.DOLocalMoveY(0, 0.5f).Play().OnPause(() =>
        {
            merchantPlace.transform.DOLocalMoveY(0, 0.5f).Play();
        });*/
        currentIncome = StageGoal.My.totalIncome;
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost, currentTimeCount, 0);
        SkipButton();
        InvokeRepeating("CheckGoal",0, 0.2f);
        yield return null;
        FTE_1_5_Manager.My.UpRole(FTE_1_5_Manager.My.npcSeed, 0.32f);
        FTE_1_5_Manager.My.UpRole(FTE_1_5_Manager.My.npcPeasant, 0.32f);
        FTE_1_5_Manager.My.UpRole(seedPlace);
        FTE_1_5_Manager.My.UpRole(merchantPlace);
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
        bornPoint.GetComponent<Building>().isBorn = false;
        FTE_1_5_Manager.My.GetComponent<RoleCreateLimit>().limitDealerCount = 0;
        yield return new WaitForSeconds(2f);
        costPanel.GetComponent<CostPanel>().HideAllCost();
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTimeCount >= limitTime)
        {
            HttpManager.My.ShowTip("超出时间限制，任务重置！");
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            Reset();
            return;
        }
        if (missiondatas.data[0].isFinish == false)
        {
            income = StageGoal.My.totalIncome - currentIncome;
            profit = income - StageGoal.My.totalCost + currentCost;
            missiondatas.data[0].currentNum = profit;
            costPanel.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost-currentCost, limitTime);
            costPanel.GetComponent<CostPanel>().ShowAllIncome(income,StageGoal.My.totalCost-currentCost);
            if (profit >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }

    void Reset()
    {
        CancelInvoke();
        NewCanvasUI.My.GamePause(false);
        //StageGoal.My.killNumber = 0;
        missiondatas.data[0].isFail = false;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].ClearWarehouse();
        }
        currentIncome = StageGoal.My.totalIncome;
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost, currentTimeCount ,0);
        InvokeRepeating("CheckGoal",0, 0.2f);
        NewCanvasUI.My.GameNormal();
    }
}