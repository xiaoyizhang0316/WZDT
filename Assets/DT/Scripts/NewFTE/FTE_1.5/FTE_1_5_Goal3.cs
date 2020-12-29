﻿using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using DG.Tweening;

public class FTE_1_5_Goal3 : BaseGuideStep
{
    //public Transform emptyPlace;
    public int costLimit;
    public int limitTime = 0;
    public int needQuality = 0;
    public Transform peasant;
    public GameObject costPanel;
    //public GameObject statPanel;
    private int count = 0;
    private int lastCost = 0;
    private int lastTimeCount = 0;
    private int currentCost = 0;
    public override IEnumerator StepStart()
    {
        Reset();
        //emptyPlace.DOMoveY(-6, 0.5f).OnComplete(() =>
        //{
        peasant.gameObject.SetActive(true);
            peasant.DOMoveY(0.32f, 1f).Play();
            peasant.GetComponent<QualityRole>().checkQuality = needQuality;
            peasant.GetComponent<QualityRole>().checkBuff = -1;
            peasant.GetComponent<QualityRole>().needCheck = true;
        //});
        InvokeRepeating("CheckGoal",0, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        peasant.GetComponent<QualityRole>().needCheck = false;
        yield return new WaitForSeconds(2f);
        costPanel.GetComponent<CostPanel>().HideAllCost();
    }

    public override bool ChenkEnd()
    {
        
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount - lastTimeCount >= limitTime)
        {
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            Reset();
            return;
        }
        
        currentCost = StageGoal.My.totalCost-lastCost ;
        costPanel.GetComponent<CostPanel>().ShowAllCost(currentCost, limitTime);
        if (currentCost >= costLimit)
        {
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            Reset();
            return;
        }
        
        if (missiondatas.data[0].isFinish == false )
        {
            missiondatas.data[0].currentNum = peasant.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        
        
        /*missiondatas.data[1].currentNum = currentCost;

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
        FTE_1_5_Manager.My.isClearGoods=true;
        missiondatas.data[0].isFail = false;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].ClearWarehouse();
        }
        lastCost = StageGoal.My.totalCost;
        lastTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(lastCost,lastTimeCount);
        InvokeRepeating("CheckGoal",0, 0.2f);
        FTE_1_5_Manager.My.isClearGoods=false;
        NewCanvasUI.My.GameNormal();
    }

    bool CheckBullet()
    {
        count = 0;
        for (int i = 0; i < peasant.GetComponent<BaseMapRole>().warehouse.Count; i++)
        {
            if (peasant.GetComponent<BaseMapRole>().warehouse[i].damage >= 80)
            {
                count++;
            }
        }

        missiondatas.data[0].currentNum = count;
        if (count >= missiondatas.data[0].maxNum)
        {
            return true;
        }

        return false;
    }
}
