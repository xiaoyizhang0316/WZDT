using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static GameEnum;
using IOIntensiveFramework.MonoSingleton;

public class CashFlow : MonoSingleton<CashFlow>
{
    public Transform cashFlowListTF;

    public GameObject cashFlowItemPrb;

    public Text cashFlowChangeText;

    /// <summary>
    /// 选项更改调用的函数
    /// </summary>
    /// <param name="num"></param>
    public void OnValueChange(CashFlowType type)
    {
        CreateTradeManager.My.selectCashFlow = type;
        SetSelectStatus();
        SetCashFlowText();
        CreateTradeManager.My.CalculateTCOfTwo(CreateTradeManager.My.currentTrade.tradeData.startRole, CreateTradeManager.My.currentTrade.tradeData.endRole);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        ClearList();
        SkillData data = GameDataMgr.My.GetSkillDataByName(CreateTradeManager.My.selectJYFS);
        for (int i = 0; i < data.supportCashFlow.Count; i++)
        {
            GameObject go = Instantiate(cashFlowItemPrb, cashFlowListTF);
            go.GetComponent<CashFlowItem>().Init(data.supportCashFlow[i]);
        }
        if (CreateTradeManager.My.currentTrade.isFirstSelect)
        {
            CashFlowType type = cashFlowListTF.GetChild(0).GetComponent<CashFlowItem>().cashFlowType;
            OnValueChange(type);
        }
        else
        {
            CreateTradeManager.My.selectCashFlow = CreateTradeManager.My.currentTrade.tradeData.selectCashFlow;
            SetCashFlowText();
            SetSelectStatus();
        }
    }

    public void ClearList()
    {
        for (int i = 0; i < cashFlowListTF.childCount; i++)
        {
            Destroy(cashFlowListTF.GetChild(i).gameObject);
        }
    }

    public int GetCurrentIndex()
    {
        for (int i = 0; i < cashFlowListTF.childCount; i++)
        {
            if (cashFlowListTF.GetChild(i).GetComponent<CashFlowItem>().cashFlowType == CreateTradeManager.My.selectCashFlow)
                return i;
        }
        return 0;
    }

    public void SetSelectStatus()
    {
        for (int i = 0; i < cashFlowListTF.childCount; i++)
        {
            if (cashFlowListTF.GetChild(i).GetComponent<CashFlowItem>().cashFlowType == CreateTradeManager.My.selectCashFlow)
            {
                cashFlowListTF.GetChild(i).GetComponent<CashFlowItem>().statusImg.sprite =
                    cashFlowListTF.GetChild(i).GetComponent<CashFlowItem>().isSelect;
            }
            else
            {
                cashFlowListTF.GetChild(i).GetComponent<CashFlowItem>().statusImg.sprite =
                    cashFlowListTF.GetChild(i).GetComponent<CashFlowItem>().normal;
            }
        }
    }

    public void SetCashFlowText()
    {
        bool isInside = true;
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.endRole));
        if (start.isNpc || end.isNpc)
        {
            isInside = false;
        }
        List<float> tempList = DealConfigData.My.GetDealConfigAdd(isInside, CreateTradeManager.My.selectCashFlow.ToString());
        List<string> result = new List<string>();
        foreach (float f in tempList)
        {
            string str = GetTCNumberString(f);
            result.Add(str);
        }
        cashFlowChangeText.text = $"搜{result[0]} 议{result[1]} 交{result[2]} 风{result[3]}";
    }

    public string GetTCNumberString(float number)
    {
        if (number == 0)
            return "+0%";

        else if (number > 0)
            return "+" + (number * 100).ToString() + "%";
        else
            return (number * 100).ToString() + "%";
    }

    public void CheckFree()
    {
        if (CreateTradeManager.My.isFree)
        {
            for (int i = 0; i < cashFlowListTF.childCount; i++)
            {
                cashFlowListTF.GetChild(i).GetComponent<CashFlowItem>().SetLock(true);
            }
        }
        else
        {
            for (int i = 0; i < cashFlowListTF.childCount; i++)
            {
                cashFlowListTF.GetChild(i).GetComponent<CashFlowItem>().SetLock(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
