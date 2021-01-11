using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using DG.Tweening;

public class FTE_1_5_Goal3_New : BaseGuideStep
{
    public int costLimit;
    public int timeLimit;
    public int needQuality;
    public Transform QM;
    public GameObject costPanel;

    private int currentCost;
    private int currentTime;
    public override IEnumerator StepStart()
    {
        currentCost = StageGoal.My.productCost;
        currentTime = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitProductCost(currentCost, currentTime, costLimit);
        NewCanvasUI.My.GamePause(false);
        QM.gameObject.SetActive(true);
        QM.DOMoveY(0.32f, 1f).Play();
        QM.GetComponent<QualityRole>().checkQuality = needQuality;
        QM.GetComponent<QualityRole>().checkBuff = -1;
        QM.GetComponent<QualityRole>().needCheck = true;
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
        currentCost = StageGoal.My.productCost;
        currentTime = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitProductCost(currentCost, currentTime, costLimit);
        missiondatas.data[0].isFail = false;
        InvokeRepeating("CheckGoal",0, 0.2f);
    }

    private void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTime > timeLimit)
        {
            missiondatas.data[0].isFail = true;
            //NewCanvasUI.My.GamePause(false);
            HttpManager.My.ShowTip("超出时间限制，任务重置！");
            Reset();
            return;
        }
        
        costPanel.GetComponent<CostPanel>().ShowProductCostAsMain(StageGoal.My.productCost-currentCost, timeLimit);
        if (StageGoal.My.productCost - currentCost > costLimit)
        {
            missiondatas.data[0].isFail = true;
            //NewCanvasUI.My.GamePause(false);
            HttpManager.My.ShowTip("超出固定成本限制，任务重置！");
            Reset();
            return;
        }

        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = QM.GetComponent<BaseMapRole>().warehouse.Count;
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
        yield return new WaitForSeconds(1.5f);
    }
}
