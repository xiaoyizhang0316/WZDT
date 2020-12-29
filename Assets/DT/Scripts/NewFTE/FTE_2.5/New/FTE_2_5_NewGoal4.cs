using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_5_NewGoal4 : BaseGuideStep
{
    public int costLimit;
    public int limitTime;
    public GameObject costPanel;
    public GameObject bornPoint;
    public GameObject dealer1;
    public GameObject place1;
    public GameObject dealer2;
    public GameObject place2;
    public GameObject dealer3;
    public GameObject place3;
    public Transform tradeMgr;
    public Transform roles;
    private int currentCost;
    private int currentTimeCount;
    public override IEnumerator StepStart()
    {
        FTE_2_5_Manager.My.isClearGoods = false;
        StageGoal.My.killNumber = 0;
        currentCost = StageGoal.My.totalCost;
        currentTimeCount = StageGoal.My.timeCount;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitDealerCount = -1;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().needLimit = true;
        NewCanvasUI.My.GamePause(false);
        dealer1.SetActive(true);
        //dealer2.SetActive(true);
        //dealer3.SetActive(true);
        dealer1.transform.DOMoveY(0.32f, 1f).Play();
        place1.transform.DOMoveY(0f, 1f).Play();
        //dealer2.transform.DOMoveY(0.32f, 1f).Play();
        //place2.transform.DOMoveY(0f, 1f).Play();
        //dealer3.transform.DOMoveY(0.32f, 1f).Play();
        //place3.transform.DOMoveY(0f, 1f).Play();
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost, currentTimeCount);
        StartCoroutine( bornPoint.GetComponent<Building>().BornEnemyForFTE_2_5(-1));
        InvokeRepeating("CheckGoal", 0.02f, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        bornPoint.GetComponent<Building>().isBornForFTE_2_5 = false;
        DoEnd();
        yield return new WaitForSeconds(1f);
        costPanel.GetComponent<CostPanel>().HideAllCost();
    }

    private void DoEnd()
    {
        /*PlayerData.My.DeleteRole(dealer1.GetComponent<BaseMapRole>().baseRoleData.ID);
        PlayerData.My.DeleteRole(dealer2.GetComponent<BaseMapRole>().baseRoleData.ID);
        PlayerData.My.DeleteRole(dealer3.GetComponent<BaseMapRole>().baseRoleData.ID);*/
        foreach (Transform trade in tradeMgr)
        {
            TradeManager.My.DeleteTrade(trade.GetComponent<TradeSign>().tradeData.ID);
        }

        foreach (Transform role in roles)
        {
            if (!role.GetComponent<BaseMapRole>().isNpc && role.gameObject.activeInHierarchy)
            {
                PlayerData.My.DeleteRole(role.GetComponent<BaseMapRole>().baseRoleData.ID);
            }
            else
            {
                role.GetComponent<BaseMapRole>().ClearWarehouse();
            }
        }

        foreach (Transform child in bornPoint.transform)
        {
            if (child.GetComponent<ConsumeSign>())
            {
                Destroy(child.gameObject);
            }
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
            Reset();
            return;
        }
        costPanel.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost - currentCost, limitTime);
        if (StageGoal.My.totalCost - currentCost >= costLimit)
        {
            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            Reset();
            return;
        }
        
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = StageGoal.My.killNumber;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        
        /*missiondatas.data[1].currentNum = StageGoal.My.totalCost - currentCost ;
        costPanel.GetComponent<CostPanel>().ShowAllCost(missiondatas.data[1].currentNum, limitTime);
        if (missiondatas.data[1].currentNum <= missiondatas.data[1].maxNum )
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
        StageGoal.My.killNumber = 0;
        missiondatas.data[0].isFail = false;
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
