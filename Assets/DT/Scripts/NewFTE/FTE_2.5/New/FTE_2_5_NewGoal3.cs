using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_NewGoal3 : BaseGuideStep
{
    public int needQuality;
    public int costLimit;
    public int limitTime = 0;
    public GameObject costPanel;
    public GameObject qualityCenter;
    public GameObject place;
    private int currentCost = 0;
    private int currentTimeCount;
    public override IEnumerator StepStart()
    {
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        NewCanvasUI.My.GamePause(false);
        qualityCenter.GetComponent<QualityRole>().checkQuality = needQuality;
        qualityCenter.GetComponent<QualityRole>().checkBuff = -1;
        qualityCenter.GetComponent<QualityRole>().needCheck = true;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost, currentTimeCount, costLimit);
        //NewCanvasUI.My.GameNormal();
        InvokeRepeating("CheckGoal", 0.02f, 0.2f);
        SkipButton();
        yield return new WaitForSeconds(0.5f);
    }
    void SkipButton()
    {
        if (needCheck && FTE_2_5_Manager.My.needSkip)
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
        qualityCenter.GetComponent<QualityRole>().needCheck = false;
        costPanel.GetComponent<CostPanel>().HideAllCost();
        //place.transform.DOMoveY(-8.32f, 0.5f);
        qualityCenter.transform.DOMoveY(-8f, 0.5f).Play().OnComplete(()=>PlayerData.My.DeleteRole(qualityCenter.GetComponent<BaseMapRole>().baseRoleData.ID));

        FTE_2_5_Manager.My.isClearGoods = true;
        yield return new WaitForSeconds(1f);
        DoEnd();
    }

    void DoEnd()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].GetComponent<BaseMapRole>().ClearWarehouse();
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish ;
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTimeCount >= limitTime)
        {
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            HttpManager.My.ShowTip("已超出时间限制，任务重置！");
            Reset();
            return;
        }
        
        costPanel.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost - currentCost, limitTime);
        if (StageGoal.My.totalCost - currentCost >= costLimit)
        {
            HttpManager.My.ShowTip("已超出成本限制，任务重置！");
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            Reset();
            return;
        }
        
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = qualityCenter.GetComponent<BaseMapRole>().warehouse.Count;
           
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        
        /*missiondatas.data[1].currentNum = StageGoal.My.totalCost - currentCost;
        costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum, limitTime);
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
        //StageGoal.My.killNumber = 0;
        FTE_2_5_Manager.My.isClearGoods=true;
        missiondatas.data[0].isFail = false;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].ClearWarehouse();
        }
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTimeCount,costLimit);
        InvokeRepeating("CheckGoal",0, 0.2f);
        FTE_2_5_Manager.My.isClearGoods=false;
        NewCanvasUI.My.GameNormal();
    }
}
