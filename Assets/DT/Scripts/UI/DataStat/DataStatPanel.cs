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
    public GameObject statItem;

    public Transform npcContent;
    public Transform otherContent;
    public Transform buildContent;
    public Transform extraContent;

    public Text totalIncome;
    public Text totalIncomePerMin;
    public Text consumeIncome;
    public Text consumeIncomePerMin;
    public Text npcNames;
    public Text npcIncome;
    public Text otherNames;
    public Text otherIncome;
    public Text totalCost;
    public Text totalCostPerMin;
    public Text tradeCost;
    public Text tradeCostPerMin;
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
    public GameObject income;
    public GameObject expend;

    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(Close);
    }

    public void Close()
    {
        statPanel.SetActive(false);
        mask.SetActive(false);
        isShow = false;
    }

    public void ShowStat()
    {
        ShowStatNew(StageGoal.My.totalIncome, StageGoal.My.consumeIncome, StageGoal.My.totalCost,
            StageGoal.My.tradeCost, StageGoal.My.npcIncomesEx,
            StageGoal.My.npcIncomes, StageGoal.My.otherIncomes,
            StageGoal.My.buildingCosts, StageGoal.My.extraCost, StageGoal.My.timeCount);
    }

    public void RefreshIncome(int totalIncome, int totalConsumeIncome, Dictionary<string, int> npcIncomesEx, Dictionary<BaseMapRole, int> npcIncomes,
        Dictionary<string, int> otherIncomes, int timeCount)
    {
        if(isShow&& !isIncomeRefreshing)
        {
            isIncomeRefreshing = true;
            ShowIncomeNew(totalIncome, totalConsumeIncome, npcIncomesEx, npcIncomes, otherIncomes, timeCount);
        }
    }

    public void RefreshExpend(int totalCost, int tradeCost, Dictionary<BaseMapRole, int> buildCost,
        Dictionary<string, int> extraCost, int timeCount)
    {
        if (isShow && !isExpendRefreshing)
        {
            isExpendRefreshing = true;
            ShowExpendNew(totalCost, tradeCost, buildCost, extraCost, timeCount);
        }
    }

    private void ShowStatNew(int totalIncome, int totalConsumeIncome, int totalCost, int tradeCost, Dictionary<string, int> npcIncomesEx,
        Dictionary<BaseMapRole, int> npcIncomes,
        Dictionary<string, int> otherIncomes, Dictionary<BaseMapRole, int> buildCost,
        Dictionary<string, int> extraCost, int timeCount)
    {
        isShow = false;
        mask.SetActive(true);
        statPanel.SetActive(true);
        gameObject.SetActive(true);
        totalIncomePerMin.text = (totalIncome * 60 / timeCount).ToString();
        this.totalIncome.text = totalIncome.ToString();
        consumeIncomePerMin.text = (totalConsumeIncome * 60 / timeCount).ToString();
        consumeIncome.text = totalConsumeIncome.ToString();
        totalCostPerMin.text = (totalCost * 60 / timeCount).ToString();
        this.totalCost.text = totalCost.ToString();
        tradeCostPerMin.text = (tradeCost * 60 / timeCount).ToString();
        this.tradeCost.text = tradeCost.ToString();
        int count = 0;
        if (npcIncomesEx.Count > 0)
        {
            int i = 0;
            foreach (var key in npcIncomesEx.Keys)
            {
                if (i >= npcContent.childCount)
                {
                    GameObject newNpc = Instantiate(statItem, npcContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key, npcIncomesEx[key] * 60 / timeCount, npcIncomesEx[key]);
                }
                npcContent.GetChild(i).GetComponent<StatItem>().Setup(key, npcIncomesEx[key] * 60 / timeCount, npcIncomesEx[key]);
                i++;
            }
            if (i < npcContent.childCount)
            {
                for (; i < npcContent.childCount; i++)
                {
                    npcContent.GetChild(i).gameObject.SetActive(false);
                }
            }
            count = i;
        }

        if (npcIncomes.Count > 0)
        {
            int i= 0;
            foreach (var key in npcIncomes.Keys)
            {
                if (i >= npcContent.childCount - count)
                {
                    GameObject newNpc = Instantiate(statItem, npcContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key.baseRoleData.baseRoleData.roleName, npcIncomes[key] * 60 / timeCount, npcIncomes[key]);
                }
                npcContent.GetChild(i + count).GetComponent<StatItem>().Setup(key.baseRoleData.baseRoleData.roleName, npcIncomes[key] * 60 / timeCount, npcIncomes[key]);
                i++;
            }
            if (i < npcContent.childCount - count)
            {
                for (; i < npcContent.childCount - count; i++)
                {
                    npcContent.GetChild(i  + count).gameObject.SetActive(false);
                }
            }
        }

        
        if (otherIncomes.Count > 0)
        {
            int i = 0;
            foreach (var key in otherIncomes.Keys)
            {
                if (i >= otherContent.childCount)
                {
                    GameObject newNpc = Instantiate(statItem, otherContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key, otherIncomes[key] * 60 / timeCount, otherIncomes[key]);
                }
                otherContent.GetChild(i).GetComponent<StatItem>().Setup(key, otherIncomes[key] * 60 / timeCount, otherIncomes[key]);
                i++;
            }
            if (i < otherContent.childCount)
            {
                for (; i < otherContent.childCount; i++)
                {
                    otherContent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        if (buildCost.Count > 0)
        {
            int i = 0;
            foreach (var key in buildCost.Keys)
            {
                if (i >= buildContent.childCount)
                {
                    GameObject newNpc = Instantiate(statItem, buildContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key.baseRoleData.baseRoleData.roleName, buildCost[key] * 60 / timeCount, buildCost[key]);
                }
                buildContent.GetChild(i).GetComponent<StatItem>().Setup(key.baseRoleData.baseRoleData.roleName, buildCost[key] * 60 / timeCount, buildCost[key]);
                i++;
            }
            if (i < buildContent.childCount)
            {
                for (; i < buildContent.childCount; i++)
                {
                    buildContent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (extraCost.Count > 0)
        {
            int i = 0;
            foreach (var key in extraCost.Keys)
            {
                if (i >= extraContent.childCount)
                {
                    GameObject newNpc = Instantiate(statItem, extraContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key, 0, extraCost[key]);
                }
                extraContent.GetChild(i).GetComponent<StatItem>().Setup(key, 0, extraCost[key]);
                i++;
            }
            if (i < extraContent.childCount)
            {
                for (; i < extraContent.childCount; i++)
                {
                    extraContent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        isShow = true;
    }

    private void ShowIncomeNew(int totalIncome, int totalConsumeIncome,Dictionary<string,int> npcIncomesEx, Dictionary<BaseMapRole, int> npcIncomes,
        Dictionary<string, int> otherIncomes, int timeCount)
    {

        totalIncomePerMin.text = (totalIncome * 60 / timeCount).ToString();
        this.totalIncome.text = totalIncome.ToString();
        consumeIncomePerMin.text = (totalConsumeIncome * 60 / timeCount).ToString();
        consumeIncome.text = totalConsumeIncome.ToString();
        int count = 0;
        if (npcIncomesEx.Count > 0)
        {
            int i = 0;
            foreach (var key in npcIncomesEx.Keys)
            {
                if (i >= npcContent.childCount)
                {
                    GameObject newNpc = Instantiate(statItem, npcContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key, npcIncomesEx[key] * 60 / timeCount, npcIncomesEx[key]);
                }
                npcContent.GetChild(i).GetComponent<StatItem>().Setup(key, npcIncomesEx[key] * 60 / timeCount, npcIncomesEx[key]);
                i++;
            }
            if (i < npcContent.childCount)
            {
                for (; i < npcContent.childCount; i++)
                {
                    npcContent.GetChild(i).gameObject.SetActive(false);
                }
            }
            count = i;
        }
        if (npcIncomes.Count > 0)
        {
            int i = 0;
            foreach (var key in npcIncomes.Keys)
            {
                if (i >= npcContent.childCount - count)
                {
                    GameObject newNpc = Instantiate(statItem, npcContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key.baseRoleData.baseRoleData.roleName, npcIncomes[key] * 60 / timeCount, npcIncomes[key]);
                }
                npcContent.GetChild(count + i).GetComponent<StatItem>().Setup(key.baseRoleData.baseRoleData.roleName, npcIncomes[key] * 60 / timeCount, npcIncomes[key]);
                i++;
            }
            if (i < npcContent.childCount - count)
            {
                for (; i < npcContent.childCount; i++)
                {
                    npcContent.GetChild(count + i).gameObject.SetActive(false);
                }
            }
        }


        if (otherIncomes.Count > 0)
        {
            int i = 0;
            foreach (var key in otherIncomes.Keys)
            {
                if (i >= otherContent.childCount)
                {
                    GameObject newNpc = Instantiate(statItem, otherContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key, otherIncomes[key] * 60 / timeCount, otherIncomes[key]);
                }
                otherContent.GetChild(i).GetComponent<StatItem>().Setup(key, otherIncomes[key] * 60 / timeCount, otherIncomes[key]);
                i++;
            }
            if (i < otherContent.childCount)
            {
                for (; i < otherContent.childCount; i++)
                {
                    otherContent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        isIncomeRefreshing = false;
    }

    private void ShowExpendNew(int totalCost, int tradeCost, Dictionary<BaseMapRole, int> buildCost,
        Dictionary<string, int> extraCost, int timeCount)
    {
        totalCostPerMin.text = (totalCost * 60 / timeCount).ToString();
        this.totalCost.text = totalCost.ToString();
        tradeCostPerMin.text = (tradeCost * 60 / timeCount).ToString();
        this.tradeCost.text = tradeCost.ToString();
        if (buildCost.Count > 0)
        {
            int i = 0;
            foreach (var key in buildCost.Keys)
            {
                if (i >= buildContent.childCount)
                {
                    GameObject newNpc = Instantiate(statItem, buildContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key.baseRoleData.baseRoleData.roleName, buildCost[key] * 60 / timeCount, buildCost[key]);
                }
                buildContent.GetChild(i).GetComponent<StatItem>().Setup(key.baseRoleData.baseRoleData.roleName, buildCost[key] * 60 / timeCount, buildCost[key]);
                i++;
            }
            if (i < buildContent.childCount)
            {
                for (; i < buildContent.childCount; i++)
                {
                    buildContent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (extraCost.Count > 0)
        {
            int i = 0;
            foreach (var key in extraCost.Keys)
            {
                if (i >= extraContent.childCount)
                {
                    GameObject newNpc = Instantiate(statItem, extraContent);
                    StatItem stat = newNpc.GetComponent<StatItem>();
                    stat.Setup(key, 0, extraCost[key]);
                }
                extraContent.GetChild(i).GetComponent<StatItem>().Setup(key, 0, extraCost[key]);
                i++;
            }
            if (i < extraContent.childCount)
            {
                for (; i < extraContent.childCount; i++)
                {
                    extraContent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        isExpendRefreshing = false;
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

    private bool isSwitch = true; 
    /// <summary>
    /// 切换收支
    /// </summary>
    public void SwitchIncome()
    {
 
        income.SetActive(true);
        expend.SetActive(false);
     
    }
    /// <summary>
    /// 切换收支
    /// </summary>
    public void SwitchOutcome()
    {
 
        income.SetActive(false);
        expend.SetActive(true);
     
    }
}
