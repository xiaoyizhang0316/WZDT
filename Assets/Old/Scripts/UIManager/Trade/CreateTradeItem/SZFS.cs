using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static GameEnum;
using IOIntensiveFramework.MonoSingleton;

public class SZFS : MonoSingleton<SZFS>
{
    public GameObject divideSlider;

    public Text currentDivideNumber;

    public Text minDivide;

    public Text maxDivide;

    public Text predictMoney;

    public Transform szfsListTF;

    public GameObject szfsItemPrb;

    public Text SZFSText;
    /// <summary>
    /// 选项更改调用的函数
    /// </summary>
    /// <param name="num"></param>
    public void OnValueChange(SZFSType type)
    {
        CreateTradeManager.My.selectSZFS = type;
        CreateTradeManager.My.CalculateTCOfTwo(CreateTradeManager.My.currentTrade.tradeData.startRole, CreateTradeManager.My.currentTrade.tradeData.endRole);
        SetSelectStatus();
        InitDivide();
        CheckSZFSMoney();
        SetTCChangeText();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        ClearList();
        SkillData data = GameDataMgr.My.GetSkillDataByName(CreateTradeManager.My.selectJYFS);
        for (int i = 0; i < data.supportSZFS.Count; i++)
        {
            GameObject go = Instantiate(szfsItemPrb, szfsListTF);
            go.GetComponent<SZFSItem>().Init(data.supportSZFS[i]);
        }
        if (CreateTradeManager.My.currentTrade.isFirstSelect)
        {
            SZFSType type = szfsListTF.GetChild(0).GetComponent<SZFSItem>().szfsType;
            OnValueChange(type);
        }
        else
        {
            CreateTradeManager.My.selectSZFS = CreateTradeManager.My.currentTrade.tradeData.selectSZFS;
            OnValueChange(CreateTradeManager.My.selectSZFS);
        }
        InitDivide();
        CheckSZFSMoney();
    }

    public void SetTCChangeText()
    {
        bool isInside = true;
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.endRole));
        if (start.isNpc || end.isNpc)
        {
            isInside = false;
        }
        List<float> tempList = DealConfigData.My.GetDealConfigAdd(isInside, CreateTradeManager.My.selectSZFS.ToString());
        List<string> result = new List<string>();
        foreach (float f in tempList)
        {
            string str = GetTCNumberString(f);
            result.Add(str);
        }
        SZFSText.text = $"搜{result[0]} 议{result[1]} 交{result[2]} 风{result[3]}";
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



    public void CheckSZFSMoney()
    {
        if (CreateTradeManager.My.selectSZFS == SZFSType.固定)
        {
            SkillData data = GameDataMgr.My.GetSkillDataByName(CreateTradeManager.My.selectJYFS);
            BaseMapRole receiveRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.receiveRole));
            int payNum = data.cost + (int)receiveRole.operationCost;
            predictMoney.text = "支付数值： " + payNum;
        }
        else if (CreateTradeManager.My.selectSZFS == SZFSType.分成)
        {
            BaseMapRole payRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.payRole));
            float per = divideSlider.GetComponent<Slider>().value / 100f;
            int payNum = (int)(payRole.monthlyProfit * per);
            predictMoney.text = "预计分成: " + payNum;
        }
        else if (CreateTradeManager.My.selectSZFS == SZFSType.剩余)
        {
            predictMoney.text = "预计支付:???";
        }
    }

    /// <summary>
    /// 初始化分成比例
    /// </summary>
    public void InitDivide()
    {
        if (CreateTradeManager.My.selectSZFS == SZFSType.分成)
        {
            divideSlider.SetActive(true);
            SkillData data = GameDataMgr.My.GetSkillDataByName(CreateTradeManager.My.selectJYFS);
            BaseMapRole payRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.payRole));
            Slider slider = divideSlider.GetComponent<Slider>();
            slider.minValue = data.baseDivide * 100;
            minDivide.text = slider.minValue.ToString();
            maxDivide.text = slider.maxValue.ToString();
            if (CreateTradeManager.My.currentTrade.isFirstSelect)
            {
                slider.value = (slider.minValue + slider.maxValue) / 2;
            }
            else
            {
                slider.value = CreateTradeManager.My.payPerc * 100;
            }
        }
        else
        {
            divideSlider.SetActive(false);
        }
    }

    public void OnDivideValueChange()
    {
        CreateTradeManager.My.payPerc = divideSlider.GetComponent<Slider>().value / 100f;
        currentDivideNumber.text = "分成比例： " + divideSlider.GetComponent<Slider>().value + "%";
        BaseMapRole payRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.payRole));
        float per = divideSlider.GetComponent<Slider>().value / 100f;
        int payNum = (int)(payRole.monthlyProfit * per);
        predictMoney.text = "预计分成: " + payNum;
    }

    public void ClearList()
    {
        for (int i = 0; i < szfsListTF.childCount; i++)
        {
            Destroy(szfsListTF.GetChild(i).gameObject);
        }
    }

    public int GetCurrentIndex()
    {
        for (int i = 0; i < szfsListTF.childCount; i++)
        {
            if (szfsListTF.GetChild(i).GetComponent<SZFSItem>().szfsType == CreateTradeManager.My.selectSZFS)
                return i;
        }
        return 0;
    }

    public void SetSelectStatus()
    {
        for (int i = 0; i < szfsListTF.childCount; i++)
        {
            if (szfsListTF.GetChild(i).GetComponent<SZFSItem>().szfsType == CreateTradeManager.My.selectSZFS)
            {
                szfsListTF.GetChild(i).GetComponent<SZFSItem>().statusImg.sprite =
                    szfsListTF.GetChild(i).GetComponent<SZFSItem>().isSelect;
            }
            else
            {
                szfsListTF.GetChild(i).GetComponent<SZFSItem>().statusImg.sprite =
                    szfsListTF.GetChild(i).GetComponent<SZFSItem>().normal;
            }
        }
    }

    public void CheckFree()
    {
        if (CreateTradeManager.My.isFree)
        {
            for (int i = 0; i < szfsListTF.childCount; i++)
            {
                szfsListTF.GetChild(i).GetComponent<SZFSItem>().SetLock(true);
                SZFSText.text = "";
            }
        }
        else
        {
            for (int i = 0; i < szfsListTF.childCount; i++)
            {
                szfsListTF.GetChild(i).GetComponent<SZFSItem>().SetLock(false);
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
        //if (CreateTradeManager.My.currentTrade != null)
        //{
        //    if (!CreateTradeManager.My.isFree)
        //    {
        //        Dropdown temp = GetComponent<Dropdown>();
        //        if (temp.value != 0)
        //        {
        //            CreateTradeManager.My.GetComponentInChildren<CashFlow>().GetComponent<Dropdown>().interactable = false;
        //            CreateTradeManager.My.GetComponentInChildren<CashFlow>().GetComponent<Dropdown>().value = 1;
        //            CreateTradeManager.My.selectCashFlow = GameEnum.CashFlowType.后钱;
        //        }
        //        else
        //        {
        //            CreateTradeManager.My.GetComponentInChildren<CashFlow>().GetComponent<Dropdown>().interactable = true;
        //        }
        //    }
        //}
    }
}
