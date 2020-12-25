using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_NewGoal3 : BaseGuideStep
{
    public int limitTime = 0;
    public GameObject costPanel;
    public GameObject qualityCenter;
    private int currentCost = 0;
    private int currentTimeCount;
    public override IEnumerator StepStart()
    {
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost, currentTimeCount);
        NewCanvasUI.My.GameNormal();
        InvokeRepeating("CheckGoal", 0.02f, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(1f);
        costPanel.GetComponent<CostPanel>().HideAllCost();
        PlayerData.My.DeleteRole(qualityCenter.GetComponent<BaseMapRole>().baseRoleData.ID);
        FTE_2_5_Manager.My.isClearGoods = true;
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
        return missiondatas.data[0].isFinish && missiondatas.data[1].isFinish;
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount - currentTimeCount >= limitTime)
        {
            missiondatas.data[0].isFinish = false;
            missiondatas.data[1].isFinish = false;
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

        
        missiondatas.data[1].currentNum = StageGoal.My.totalCost - currentCost;
        costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum);
        if (missiondatas.data[1].currentNum <= missiondatas.data[1].maxNum)
        {
            missiondatas.data[1].isFinish = true;
        }
        else
        {
            missiondatas.data[0].isFinish = false;
            missiondatas.data[1].isFinish = false;
            Reset();
        }
    }
    
    void Reset()
    {
        CancelInvoke();
        NewCanvasUI.My.GamePause(false);
        //StageGoal.My.killNumber = 0;
        FTE_2_5_Manager.My.isClearGoods=true;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].ClearWarehouse();
        }
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTimeCount);
        InvokeRepeating("CheckGoal",0, 0.2f);
        FTE_2_5_Manager.My.isClearGoods=false;
        NewCanvasUI.My.GameNormal();
    }
}
