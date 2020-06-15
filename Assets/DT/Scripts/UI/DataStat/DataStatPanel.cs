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
    public Text npcNames;
    public Text npcIncome;
    public Text otherNames;
    public Text otherIncome;
    public Text totalCost;
    public Text tradeCost;
    public Text buildingNames;
    public Text buildingCost;
    public Text extraNames;
    public Text extraCost;

    private string npcNamesStr;
    private string npcIncomeStr;
    private string otherNamesStr;
    private string otherIncomeStr;
    private string buildingNamesStr;
    private string buildCostStr;
    private string extraNamesStr;
    private string extraCostStr;

    public bool isShow = false;
    private bool isIncomeRefreshing = false;
    private bool isExpendRefreshing = false;

    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(Close);
    }

    void Close()
    {
        statPanel.SetActive(false);
        mask.SetActive(false);
        isShow = false;
    }

    public void ShowStat()
    {
        ShowStat(StageGoal.My.totalIncome, StageGoal.My.consumeIncome, StageGoal.My.totalCost,
            StageGoal.My.tradeCost,
            StageGoal.My.npcIncomes, StageGoal.My.otherIncomes,
            StageGoal.My.buildingCosts, StageGoal.My.extraCost, StageGoal.My.timeCount);
    }

    public void RefreshIncome(int totalIncome, int totalConsumeIncome, Dictionary<BaseMapRole, int> npcIncomes,
        Dictionary<string, int> otherIncomes, int timeCount)
    {
        if(isShow&& !isIncomeRefreshing)
        {
            isIncomeRefreshing = true;
            ShowIncome(totalIncome, totalConsumeIncome, npcIncomes, otherIncomes, timeCount);
        }
    }

    public void RefreshExpend(int totalCost, int tradeCost, Dictionary<BaseMapRole, int> buildCost,
        Dictionary<string, int> extraCost, int timeCount)
    {
        if (isShow && !isExpendRefreshing)
        {
            isExpendRefreshing = true;
            ShowExpend(totalCost, tradeCost, buildCost, extraCost, timeCount);
        }
    }



    private void ShowStat(int totalIncome,int totalConsumeIncome, int totalCost, int tradeCost,
        Dictionary<BaseMapRole, int> npcIncomes,
        Dictionary<string, int> otherIncomes, Dictionary<BaseMapRole, int> buildCost, 
        Dictionary<string, int> extraCost, int timeCount)
    {
        isShow = false;
        mask.SetActive(true);
        statPanel.SetActive(true);

        npcNamesStr = "";
        npcIncomeStr = "";
        otherNamesStr = "";
        otherIncomeStr = "";
        buildingNamesStr = "";
        buildCostStr = "";
        extraNamesStr = "";
        extraCostStr = "";
        this.totalIncome.text = string.Format($"{totalIncome*60/timeCount}\t\t{totalIncome}");
        consumeIncome.text = string.Format($"{totalConsumeIncome * 60 / timeCount}\t\t{totalConsumeIncome}");
        this.totalCost.text = string.Format($"{totalCost*60/timeCount}\t\t{totalCost}");
        this.tradeCost.text = string.Format($"{tradeCost*60/timeCount}\t\t{tradeCost}");
        if (npcIncomes.Count > 0)
        {
            foreach(var k in npcIncomes.Keys)
            {
                npcNamesStr += string.Format($"{k.baseRoleData.baseRoleData.roleName}\n");
                npcIncomeStr += string.Format($"{npcIncomes[k] * 60 / timeCount}  \t\t{npcIncomes[k]}\n");
            }
        }
        else
        {
            npcNamesStr = "None";
        }
        npcNames.text = npcNamesStr;
        npcIncome.text = npcIncomeStr;

        if (otherIncomes.Count > 0)
        {
            foreach(var k in otherIncomes.Keys)
            {
                otherNamesStr += string.Format($"{k}\n");
                otherIncomeStr += string.Format($"{otherIncomes[k] * 60 / timeCount}  \t\t{otherIncomes[k]}\n");
            }
        }
        else
        {
            otherNamesStr = "None";
        }
        otherNames.text = otherNamesStr;
        otherIncome.text = otherIncomeStr;

        if (buildCost.Count > 0)
        {
            foreach (var k in buildCost.Keys)
            {
                buildingNamesStr += string.Format($"{k.baseRoleData.baseRoleData.roleName}\n");
                buildCostStr += string.Format($"{buildCost[k] * 60 / timeCount}  \t\t{buildCost[k]}\n");
            }
        }
        else
        {
            buildingNamesStr = "None";
        }
        buildingNames.text = buildingNamesStr;
        buildingCost.text = buildCostStr;

        if (extraCost.Count > 0)
        {
            foreach (var k in otherIncomes.Keys)
            {
                extraNamesStr += string.Format($"{k}\n");
                extraCostStr += string.Format($"{extraCost[k] * 60 / timeCount}  \t\t{extraCost[k]}\n");
            }
        }
        else
        {
            extraNamesStr = "None";
        }
        extraNames.text = extraNamesStr;
        this.extraCost.text = extraCostStr;
        isShow = true;
    }

    private void ShowIncome(int totalIncome, int totalConsumeIncome, Dictionary<BaseMapRole, int> npcIncomes,
        Dictionary<string, int> otherIncomes, int timeCount)
    {
        npcNamesStr = "";
        npcIncomeStr = "";
        otherNamesStr = "";
        otherIncomeStr = "";
        this.totalIncome.text = string.Format($"{totalIncome * 60 / timeCount}\t\t{totalIncome}");
        consumeIncome.text = string.Format($"{totalConsumeIncome * 60 / timeCount}\t\t{totalConsumeIncome}");
        if (npcIncomes.Count > 0)
        {
            foreach (var k in npcIncomes.Keys)
            {
                npcNamesStr += string.Format($"{k.baseRoleData.baseRoleData.roleName}\n");
                npcIncomeStr += string.Format($"{npcIncomes[k] * 60 / timeCount}  \t\t{npcIncomes[k]}\n");
            }
        }
        else
        {
            npcNamesStr = "None";
        }
        npcNames.text = npcNamesStr;
        npcIncome.text = npcIncomeStr;

        if (otherIncomes.Count > 0)
        {
            foreach (var k in otherIncomes.Keys)
            {
                otherNamesStr += string.Format($"{k}\n");
                otherIncomeStr += string.Format($"{otherIncomes[k] * 60 / timeCount}  \t\t{otherIncomes[k]}\n");
            }
        }
        else
        {
            otherNamesStr = "None";
        }
        otherNames.text = otherNamesStr;
        otherIncome.text = otherIncomeStr;

        isIncomeRefreshing = false;
    }

    private void ShowExpend(int totalCost, int tradeCost, Dictionary<BaseMapRole, int> buildCost,
        Dictionary<string, int> extraCost, int timeCount)
    {
        buildingNamesStr = "";
        buildCostStr = "";
        extraNamesStr = "";
        extraCostStr = "";
        this.totalCost.text = string.Format($"{totalCost * 60 / timeCount}\t\t{totalCost}");
        this.tradeCost.text = string.Format($"{tradeCost * 60 / timeCount}\t\t{tradeCost}");

        if (buildCost.Count > 0)
        {
            foreach (var k in buildCost.Keys)
            {
                buildingNamesStr += string.Format($"{k.baseRoleData.baseRoleData.roleName}\n");
                buildCostStr += string.Format($"{buildCost[k] * 60 / timeCount}  \t\t{buildCost[k]}\n");
            }
        }
        else
        {
            buildingNamesStr = "None";
        }
        buildingNames.text = buildingNamesStr;
        buildingCost.text = buildCostStr;

        if (extraCost.Count > 0)
        {
            foreach (var k in extraCost.Keys)
            {
                extraNamesStr += string.Format($"{k}\n");
                extraCostStr += string.Format($"{extraCost[k] * 60 / timeCount}  \t\t{extraCost[k]}\n");
            }
        }
        else
        {
            extraNamesStr = "None";
        }
        extraNames.text = extraNamesStr;
        this.extraCost.text = extraCostStr;

        isExpendRefreshing = false;
    }
}
