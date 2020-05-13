using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayRelationship : MonoBehaviour
{

    public Text isFreeText;
    /// <summary>
    /// 当选择是否免费时
    /// </summary>
    public void OnValueChange()
    {
        CreateTradeManager.My.isFree = GetComponentInChildren<Toggle>().isOn;
        CreateTradeManager.My.CalculateTCOfTwo(CreateTradeManager.My.currentTrade.tradeData.startRole, CreateTradeManager.My.currentTrade.tradeData.endRole);
        CheckFree();
    }

    /// <summary>
    /// 当改变付钱角色时
    /// </summary>
    public void OnPayRoleChange()
    {
        
    }

    /// <summary>
    /// 改变收钱角色时
    /// </summary>
    public void OnReceiveRoleChange()
    {

    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(CreateTradeManager.My.selectJYFS);
        if (data.supportFree)
        {
            GetComponentInChildren<Toggle>().interactable = true;
        }
        else
        {
            GetComponentInChildren<Toggle>().interactable = false;
            CreateTradeManager.My.isFree = false;
        }
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.startRole));
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.endRole));
        if (!startRole.isNpc && !endRole.isNpc)
        {
            GetComponentInChildren<Toggle>().interactable = true;
        }
        GetComponentInChildren<Toggle>().isOn = CreateTradeManager.My.isFree;
        CheckFree();
    }

    public void CheckFree()
    {
        SZFS.My.CheckFree();
        CashFlow.My.CheckFree();
        if (CreateTradeManager.My.isFree)
        {
            bool isInside = true;
            BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.startRole));
            BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.endRole));
            if (start.isNpc || end.isNpc)
            {
                isInside = false;
            }
            GetComponentsInChildren<Dropdown>()[0].interactable = false;
            GetComponentsInChildren<Dropdown>()[1].interactable = false;
            List<float> tempList = DealConfigData.My.GetDealConfigAdd(isInside, "免费");
            List<string> result = new List<string>();
            foreach (float f in tempList)
            {
                string str = GetTCNumberString(f);
                result.Add(str);
            }
            isFreeText.text = $"搜{result[0]} 议{result[1]} 交{result[2]} 风{result[3]}";
        }
        else
        {
            SZFS.My.SetTCChangeText();
            CashFlow.My.SetCashFlowText();
            GetComponentsInChildren<Dropdown>()[0].interactable = true;
            GetComponentsInChildren<Dropdown>()[1].interactable = true;

        }
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
