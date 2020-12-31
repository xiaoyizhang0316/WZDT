using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CostPanel : MonoBehaviour
{
    public GameObject costImage;
    public Text totalCostText;
    public Text tradeCostText;
    public Text otherCostText;
    public Text timeCostText;
    private static string totalTextString = "总成本：";
    private static string tradeTextString = "交易的成本：";
    private static string otherTextString = "其他成本：";
    private static string timeTextString = "剩余时间：";

    private int startTotalCost;
    private int startTradeCost;
    private int startOtherCost;
    private int startTimeCount;
    private int currentPeriod;

    public GameObject incomeImage;
    public Text totalIncomeText;
    public Text profitText;
    private static string totalIncomeString = "总收入：";
    private static string profitString = "利润：";

    public GameObject equipPanel;
    public GameObject missionPanel;

    public void InitCostPanel(int cost, int timeCount)
    {
        totalCostText.text = totalTextString;
        tradeCostText.text = tradeTextString;
        otherCostText.text = otherTextString;

        startTotalCost = cost;
        startTradeCost = StageGoal.My.tradeCost;
        startOtherCost = StageGoal.My.productCost + StageGoal.My.extraCosts;
        startTimeCount = timeCount;
    }

    public void ShowAllCost(int totalCost, int limitTime=-1)
    {
        currentPeriod = StageGoal.My.timeCount - startTimeCount;
        
        costImage.SetActive(true);

        totalCostText.text = totalTextString + totalCost;
        tradeCostText.text = tradeTextString +
            (StageGoal.My.tradeCost - startTradeCost);
        otherCostText.text = otherTextString +
                             (StageGoal.My.productCost + StageGoal.My.extraCosts - startOtherCost);
        if (limitTime == -1)
        {
            timeCostText.text = timeTextString +"不限时";
        }
        else
        {
            timeCostText.text = timeTextString + (limitTime - currentPeriod) + " s";
        }
    }

    public void ShowAllIncome(int income, int outcome)
    {
        incomeImage.SetActive(true);
        totalIncomeText.text = totalIncomeString + income;
        profitText.text = profitString + (income - outcome);
    }

    public void HideAllCost()
    {
        costImage.SetActive(false);
        incomeImage.SetActive(false);
    }

    private bool needShow = false;
    private void Update()
    {
        if (equipPanel != null && equipPanel.activeInHierarchy)
        {
            if (missionPanel != null && missionPanel.activeInHierarchy)
            {
                missionPanel.transform.DOScale(Vector3.zero, 0.02f).Play();
            }

            transform.DOScale(Vector3.zero, 0.02f).Play();

        }
        else
        {
            if (missionPanel.transform.localScale == Vector3.zero)
            {
                missionPanel.transform.DOScale(Vector3.one, 0.02f).Play();
            }

            if (transform.localScale == Vector3.zero)
            {
                transform.DOScale(Vector3.one, 0.02f).Play();
            }
        }
    }
}
