using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FTE_1_5_Goal5 : BaseGuideStep
{
    public GameObject fruitQT;
    public GameObject place;
    private int currentIncome = 0;
    private int currentCost = 0;
    public GameObject costImage;
    public GameObject bornPoint;
    public override IEnumerator StepStart()
    {
        //fruitQT.SetActive(false);
        StartCoroutine( bornPoint.GetComponent<Building>().BornEnemy1(30));
        Debug.LogWarning(fruitQT.GetComponent<BaseMapRole>().baseRoleData.ID);
        PlayerData.My.DeleteRole(fruitQT.GetComponent<BaseMapRole>().baseRoleData.ID);
        NewCanvasUI.My.GamePause(false);
        //Destroy(place);
        currentIncome = StageGoal.My.totalIncome;
        currentCost = StageGoal.My.totalCost;
        costImage.GetComponent<CostPanel>().InitCostPanel(currentCost, StageGoal.My.timeCount);
        //StageGoal.My.totalIncome = 0;
        //NewGuideManager.My.BornEnemy1(30);
        //NewGuideManager.My.BornEnemy1(30);
        InvokeRepeating("CheckGoal",0, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        bornPoint.GetComponent<Building>().isBorn = false;
        yield return new WaitForSeconds(2f);
        costImage.GetComponent<CostPanel>().HideAllCost();
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = StageGoal.My.totalIncome-currentIncome-StageGoal.My.totalCost+currentCost;
            costImage.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost-currentCost);
            costImage.GetComponent<CostPanel>().ShowAllIncome(StageGoal.My.totalIncome-currentIncome, StageGoal.My.totalCost-currentCost);
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }
}
