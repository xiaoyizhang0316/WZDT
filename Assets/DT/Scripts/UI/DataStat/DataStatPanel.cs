using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IOIntensiveFramework.MonoSingleton;

public class DataStatPanel : MonoSingleton<DataStatPanel>
{
    public GameObject statPanel;
    public GameObject mask;
    public Button closeBtn;

    public Text totalIncome;
    public Text consumeIncome;
    public Text npcIncome;
    public Text otherIncome;
    public Text totalCost;
    public Text tradeCost;
    public Text buildingCost;
    public Text extraCost;

    private string npcIncomeStr;
    private string otherIncomeStr;
    private string buildCostStr;
    private string extraCostStr;

    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(Close);
    }

    void Close()
    {
        statPanel.SetActive(false);
        mask.SetActive(false);
    }

    public void ShowStat()
    {
        ShowStat(StageGoal.My.totalIncome, StageGoal.My.consumeIncome, StageGoal.My.totalCost,
            StageGoal.My.tradeCost,
            StageGoal.My.npcIncomes, StageGoal.My.otherIncomes,
            StageGoal.My.buildingCosts, StageGoal.My.extraCost, StageGoal.My.timeCount);
    }

    private void ShowStat(int totalIncome,int totalConsumeIncome, int totalCost, int tradeCost,
        Dictionary<BaseMapRole, int> npcIncomes,
        Dictionary<string, int> otherIncomes, Dictionary<BaseMapRole, int> buildCost, 
        Dictionary<string, int> extraCost, int timeCount)
    {
        mask.SetActive(true);
        statPanel.SetActive(true);
        npcIncomeStr = "";
        otherIncomeStr = "";
        buildCostStr = "";
        extraCostStr = "";
        this.totalIncome.text = string.Format($"{totalIncome*60/timeCount}/min\t\t{totalIncome}");
        consumeIncome.text = string.Format($"{totalConsumeIncome * 60 / timeCount}/min\t\t{totalConsumeIncome}");
        this.totalCost.text = string.Format($"{totalCost*60/timeCount}/min\t\t{totalCost}");
        this.tradeCost.text = string.Format($"{tradeCost*60/timeCount}/min\t\t{tradeCost}");
        if (npcIncomes.Count > 0)
        {
            foreach(var k in npcIncomes.Keys)
            {
                npcIncomeStr += string.Format($"{k.baseRoleData.baseRoleData.roleName}\t{npcIncomes[k] * 60 / timeCount}/min\t{npcIncomes[k]}\n");
            }
        }
        else
        {
            npcIncomeStr = "None";
        }

        npcIncome.text = npcIncomeStr;

        if (otherIncomes.Count > 0)
        {
            foreach(var k in otherIncomes.Keys)
            {
                otherIncomeStr += string.Format($"{k}\t{otherIncomes[k] * 60 / timeCount}/min\t{otherIncomes[k]}\n");
            }
        }
        else
        {
            otherIncomeStr = "None";
        }

        otherIncome.text = otherIncomeStr;

        if (buildCost.Count > 0)
        {
            foreach (var k in buildCost.Keys)
            {
                buildCostStr += string.Format($"{k.baseRoleData.baseRoleData.roleName}\t{buildCost[k] * 60 / timeCount}/min\t{buildCost[k]}\n");
            }
        }
        else
        {
            buildCostStr = "None";
        }

        buildingCost.text = buildCostStr;

        if (extraCost.Count > 0)
        {
            foreach (var k in otherIncomes.Keys)
            {
                extraCostStr += string.Format($"{k}\t{extraCost[k] * 60 / timeCount}/min\t{extraCost[k]}\n");
            }
        }
        else
        {
            extraCostStr = "None";
        }

        this.extraCost.text = extraCostStr;
    }
}
