using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostPanel : MonoBehaviour
{
    public GameObject costImage;
    public Text totalCostText;
    public Text tradeCostText;
    public Text otherCostText;
    private static string totalTextString = "总的周期成本：";
    private static string tradeTextString = "交易的周期成本：";
    private static string otherTextString = "其他的周期成本：";

    private int startTotalCost;
    private int startTradeCost;
    private int startOtherCost;
    private int startTimeCount;
    private int currentPeriod;

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

    public void ShowAllCost(int totalCost)
    {
        currentPeriod = StageGoal.My.timeCount - startTimeCount;
        if (currentPeriod==0)
        {
            return;
        }
        costImage.SetActive(true);

        totalCostText.text = totalTextString + totalCost;
        tradeCostText.text = tradeTextString +
                             (StageGoal.My.tradeCost - startTradeCost)*60 / currentPeriod;
        otherCostText.text = otherTextString +
                             (StageGoal.My.productCost + StageGoal.My.extraCosts - startOtherCost)*60 / currentPeriod;
    }

    public void HideAllCost()
    {
        costImage.SetActive(false);
    }
}
