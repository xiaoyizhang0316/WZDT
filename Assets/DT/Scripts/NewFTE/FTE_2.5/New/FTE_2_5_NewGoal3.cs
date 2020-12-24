using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_NewGoal3 : BaseGuideStep
{
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
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = qualityCenter.GetComponent<BaseMapRole>().warehouse.Count;
            missiondatas.data[1].currentNum = (StageGoal.My.totalCost - currentCost) * 60 /
                                              ((StageGoal.My.timeCount - currentTimeCount) == 0
                                                  ? 1
                                                  : (StageGoal.My.timeCount - currentTimeCount));
            costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum);
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        if (missiondatas.data[0].isFinish&& missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = (StageGoal.My.totalCost - currentCost) * 60 /
                                              ((StageGoal.My.timeCount - currentTimeCount) == 0
                                                  ? 1
                                                  : (StageGoal.My.timeCount - currentTimeCount));
            costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum);
            if (missiondatas.data[1].currentNum <= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }
    }
}
