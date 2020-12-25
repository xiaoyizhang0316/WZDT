using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_NewGoal1 : BaseGuideStep
{
    public int limitTime = 40;
    public GameObject qualityCenter;
    public GameObject costPanel;
    public GameObject openCG;

    public bool isEnd;
    public override IEnumerator StepStart()
    {
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
        InvokeRepeating("CheckGoal", 0.02f, 0.2f);
        costPanel.GetComponent<CostPanel>().InitCostPanel(0,0);
        isEnd = false;
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(0.5f);
        costPanel.GetComponent<CostPanel>().HideAllCost();
        //qualityCenter.GetComponent<BaseMapRole>().ClearWarehouse();
        DoEnd();
        FTE_2_5_Manager.My.isClearGoods = true;
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
        return isEnd;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = qualityCenter.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }

           
        }
        
        missiondatas.data[1].currentNum = StageGoal.My.totalCost ;
        costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum, limitTime);

        if (missiondatas.data[1].currentNum > missiondatas.data[1].maxNum)
        {
            NewCanvasUI.My.GamePause(false);
            missiondatas.data[1].isFinish = false;
            openCG.SetActive(true);
        }
        else
        {
            missiondatas.data[1].isFinish = true;
        }
    }
}
