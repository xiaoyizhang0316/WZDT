using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_NewGoal1 : BaseGuideStep
{
    public int needQuality;
    public int costLimit;
    public int limitTime = 40;
    //public GameObject qualityCenter;
    //public GameObject place;
    public GameObject costPanel;
    public GameObject openCG;

    public GameObject rectBorder;
    //public Transform tradeMgr;
    private int currentTime = 0;
    private int currentCost = 0;

    public bool isEnd;
    public override IEnumerator StepStart()
    {
        StageGoal.My.maxRoleLevel = 1;
        //RoleSet();
        NewCanvasUI.My.GamePause(false);
        currentTime = StageGoal.My.timeCount;
        currentCost = StageGoal.My.totalCost;
        FTE_2_5_Manager.My.qualityStation.SetActive(true);
        /*qualityCenter.transform.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            qualityCenter.transform.DOLocalMoveY(0.32f, 1f).Play();
        });*/
        FTE_2_5_Manager.My.qualityStation.GetComponent<QualityRole>().checkQuality = needQuality;
        FTE_2_5_Manager.My.qualityStation.GetComponent<QualityRole>().checkBuff = -1;
        FTE_2_5_Manager.My.qualityStation.GetComponent<QualityRole>().needCheck = true;
        FTE_2_5_Manager.My.qualityStation.GetComponent<QualityRole>().QualityReset();
        //place.transform.DOMoveY(0f, 1f);
        InvokeRepeating("CheckGoal", 0.02f, 0.2f);
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTime, 0);
        isEnd = false;
        SkipButton();
        yield return new WaitForSeconds(0.5f);
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.qualityStation, 0.32f);
    }
    
    void SkipButton()
    {
        if (needCheck && FTE_2_5_Manager.My.needSkip)
        {
            if (endButton != null)
            {
                
                endButton.onClick.AddListener(() =>
                {
                    isEnd = true;
                });
                endButton.interactable = true;
                endButton.gameObject.SetActive(true);
            }
        }
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        FTE_2_5_Manager.My.qualityStation.GetComponent<QualityRole>().needCheck = false;
        costPanel.GetComponent<CostPanel>().HideAllCost();
        //FTE_2_5_Manager.My.isClearGoods = true;
        TradeManager.My.ResetAllTrade();
        yield return new WaitForSeconds(2f);
        DoEnd();
    }

    void DoEnd()
    {
        PlayerData.My.ClearAllRoleWarehouse();
    }

    public override bool ChenkEnd()
    {
        if(isEnd)
            return !openCG.activeInHierarchy;
        return false;
    }

    void Reset()
    {
        //CancelInvoke();
        currentTime = StageGoal.My.timeCount;
        currentCost = StageGoal.My.totalCost;
        costPanel.GetComponent<CostPanel>().InitCostPanel(currentCost,currentTime, 0);
        PlayerData.My.ClearAllRoleWarehouse();
        TradeManager.My.ResetAllTrade();
        FTE_2_5_Manager.My.qualityStation.GetComponent<QualityRole>().QualityReset();
        missiondatas.data[0].isFail = false;
        missiondatas.data[0].currentNum = 0;
        
        InvokeRepeating("CheckGoal", 0.02f, 0.2f);
    }

    void CheckGoal()
    {
        if (StageGoal.My.timeCount -currentTime>= limitTime)
        {
            //NewCanvasUI.My.GamePause(false);

            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            //openCG.SetActive(true);
            //isEnd = true;
            CancelInvoke();
            Reset();
            return;
        }
        costPanel.GetComponent<CostPanel>().ShowAllCost(StageGoal.My.totalCost-currentCost, limitTime);
        /*if (StageGoal.My.totalCost >= costLimit)
        {
            NewCanvasUI.My.GamePause(false);
            HttpManager.My.ShowTip("已超出成本限制，任务失败！");

            missiondatas.data[0].isFail = true;
            missiondatas.data[0].isFinish = false;
            openCG.SetActive(true);
            CancelInvoke();
            isEnd = true;
            return;
        }*/
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = FTE_2_5_Manager.My.qualityStation.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                MissionData missionData = new MissionData();
                missionData.content = "成本控制在" + costLimit + "以内";
                missionData.currentNum = StageGoal.My.totalCost-currentCost;
                missionData.maxNum = costLimit;
                missionData.isFail = true;
                MissionManager.My.AddMission(missionData, missionTitle);
                rectBorder.SetActive(true);
                if (StageGoal.My.totalCost > costLimit)
                {
                    HttpManager.My.ShowTip("超出成本限制，任务失败！");
                }
                missiondatas.data[0].isFinish = true;
                openCG.SetActive(true);
                isEnd = true;
                CancelInvoke();
            }
        }
        
        /*missiondatas.data[1].currentNum = StageGoal.My.totalCost ;
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
        }*/
    }

    void RoleSet()
    {
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).effect = 15;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).efficiency = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).tradeCost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).riskResistance = 120;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).cost = 100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).upgradeCost = 1000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).effect = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).efficiency = 25;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).tradeCost = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).riskResistance = 240;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).cost = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).upgradeCost = 2000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).effect = 25;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).efficiency = 30;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).tradeCost = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).riskResistance = 360;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).cost = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).upgradeCost = 3000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).effect = 35;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).efficiency = 35;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).tradeCost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).riskResistance = 480;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).cost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).upgradeCost = 4000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).effect = 45;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).efficiency = 40;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).tradeCost = 1200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).riskResistance = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).cost = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).upgradeCost = 5000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).effect = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).efficiency = 10;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).tradeCost = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).riskResistance = 110;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).cost = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).upgradeCost = 1000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).effect = 25;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).efficiency = 15;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).tradeCost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).riskResistance = 220;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).cost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).upgradeCost = 2000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).effect = 30;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).efficiency = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).tradeCost = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).riskResistance = 330;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).cost = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).upgradeCost = 3000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).effect = 40;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).efficiency = 25;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).tradeCost = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).riskResistance = 440;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).cost = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).upgradeCost = 4000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).effect = 50;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).efficiency = 30;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).tradeCost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).riskResistance = 550;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).cost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).upgradeCost = 5000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).effect = 60;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).efficiency = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).tradeCost = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).riskResistance = 100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).cost = 100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).upgradeCost = 1000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).effect = 75;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).efficiency = 28;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).tradeCost = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).riskResistance = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).cost = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).upgradeCost = 2000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).effect = 90;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).efficiency = 35;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).tradeCost = 700;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).riskResistance = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).cost = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).upgradeCost =3000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).effect = 105;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).efficiency = 42;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).tradeCost = 900;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).riskResistance = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).cost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).upgradeCost =4000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).effect = 120;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).efficiency = 50;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).tradeCost = 1100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).riskResistance = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).cost = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).upgradeCost = 5000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).efficiency = 30;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).range = 28;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).tradeCost = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).riskResistance = 100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).cost = 100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).upgradeCost = 1000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).efficiency = 35;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).range = 32;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).tradeCost = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).riskResistance = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).cost = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).upgradeCost = 2000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).efficiency = 40;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).range = 36;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).tradeCost = 700;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).riskResistance = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).cost = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).upgradeCost = 3000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).efficiency = 45;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).range = 40;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).tradeCost = 900;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).riskResistance = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).cost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).upgradeCost = 4000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).efficiency = 50;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).range = 44;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).tradeCost = 1100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).riskResistance = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).cost = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).upgradeCost = 5000;
    }
}
