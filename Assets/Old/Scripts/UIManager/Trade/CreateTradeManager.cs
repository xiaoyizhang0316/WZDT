using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class CreateTradeManager : MonoSingleton<CreateTradeManager>
{
    /// <summary>
    /// 当前的交易
    /// </summary>
    public TradeSign currentTrade;

    /// <summary>
    /// 选择的现金流结构
    /// </summary>
    public CashFlowType selectCashFlow;

    /// <summary>
    /// 释放技能者
    /// </summary>
    public string castRole;

    /// <summary>
    /// 技能目标
    /// </summary>
    public string targetRole;

    /// <summary>
    /// 发起者姓名
    /// </summary>
    public Text startName;

    /// <summary>
    /// 承受者姓名
    /// </summary>
    public Text endName;

    /// <summary>
    /// 发起者角色图标
    /// </summary>
    public Image startLogo;

    /// <summary>
    /// 承受者角色图标
    /// </summary>
    public Image endLogo;

    /// <summary>
    /// 发起者panel
    /// </summary>
    public Transform startRolePanel;

    /// <summary>
    /// 承受者panel
    /// </summary>
    public Transform endRolePanel;

    public Button moneyFirstButton;

    public Button moneyLastButton;

    public Text tradeCostText;

    /// <summary>
    /// 打开并初始化
    /// </summary>
    public void Open(GameObject tradeGo)
    {
        Show();
        currentTrade = tradeGo.GetComponent<TradeSign>() ;
        Init();
        startRolePanel.transform.localPosition = new Vector3(0f, startRolePanel.transform.localPosition.y, startRolePanel.transform.localPosition.z);
        endRolePanel.transform.localPosition = new Vector3(0f, endRolePanel.transform.localPosition.y, endRolePanel.transform.localPosition.z);
    }

    /// <summary>
    /// 初始化 
    /// </summary>
    public void Init()
    {
        selectCashFlow = currentTrade.tradeData.selectCashFlow;
        castRole = currentTrade.tradeData.castRole;
        targetRole = currentTrade.tradeData.targetRole;
        InitName();
        InitRoleInfo();
        InitCashFlow();
        InitTradeCost();
    }

    /// <summary>
    /// 初始化发起者和承受者名字
    /// </summary>
    public void InitName()
    {
        //startName.text = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.startRole)).baseRoleData.baseRoleData.roleName;
        //endName.text = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.endRole)).baseRoleData.baseRoleData.roleName;
        //string startType = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.startRole)).baseRoleData.baseRoleData.roleType.ToString();
        //string endType = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.endRole)).baseRoleData.baseRoleData.roleType.ToString();
        //startLogo.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + startType);
        //endLogo.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + endType);
    }

    /// <summary>
    /// 初始化发起者和承受者的信息
    /// </summary>
    public void InitRoleInfo()
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.endRole));
        endRolePanel.Find("EndRoleTradeCost").GetComponent<Text>().text = end.baseRoleData.tradeCost.ToString();
        endRolePanel.Find("EndRoleRisk").GetComponent<Text>().text = end.baseRoleData.riskResistance.ToString();
        startRolePanel.Find("StartRoleTradeCost").GetComponent<Text>().text = start.baseRoleData.tradeCost.ToString();
        startRolePanel.Find("StartRoleRisk").GetComponent<Text>().text = start.baseRoleData.riskResistance.ToString();
        startName.text = start.baseRoleData.baseRoleData.roleName;
        endName.text = end.baseRoleData.baseRoleData.roleName;
        startRolePanel.transform.DOLocalMoveX(-220f,0.5f).Play().timeScale = 1f / DOTween.timeScale;
        endRolePanel.transform.DOLocalMoveX(220f, 0.5f).Play().timeScale = 1f / DOTween.timeScale;
    }

    /// <summary>
    /// 初始化交易成本
    /// </summary>
    public void InitTradeCost()
    {
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.startRole));
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.endRole));
        int result = startRole.baseRoleData.tradeCost + endRole.baseRoleData.tradeCost;
        result += startRole.baseRoleData.riskResistance + endRole.baseRoleData.riskResistance;
        int result1, result2;
        if (startRole.isNpc || endRole.isNpc)
        {
            result1 = (int)(result * 0.6f);
            result2 = (int)(result * 0.3f);
        }
        else
        {
            result1 = (int)(result * 0.5f);
            result2 = (int)(result * 0.2f);
        }
        if (currentTrade.isTradeSettingBest())
        {
            result = result2;
        }
        else
        {
            result = result1;
        }
        tradeCostText.text = result.ToString();
    }

    /// <summary>
    /// 初始化现金流结构设置
    /// </summary>
    public void InitCashFlow()
    {
        if (selectCashFlow == CashFlowType.先钱)
        {
            OnCashFlowValueChange(0);
        }
        else if (selectCashFlow == CashFlowType.后钱)
        {
            OnCashFlowValueChange(1);
        }
    }

    /// <summary>
    /// 当现金流结构选项改变时
    /// </summary>
    /// <param name="num"></param>
    public void OnCashFlowValueChange(int num)
    {
        if (num == 0)
        {
            selectCashFlow = CashFlowType.先钱;
            moneyLastButton.interactable = true;
            moneyFirstButton.interactable = false;
        }
        else if(num == 1)
        {
            selectCashFlow = CashFlowType.后钱;
            moneyFirstButton.interactable = true;
            moneyLastButton.interactable = false;
        }
        if (selectCashFlow == currentTrade.tradeData.selectCashFlow)
        {
            InitTradeCost();
        }
        else
        {
            tradeCostText.text = "???";
        }
    }

    /// <summary>
    /// 保存并退出
    /// </summary>
    public void SaveAndQuit()
    {
        SaveTradeData();
        DeleteTradeMenu();
        RecordChangeTrade(currentTrade);
    }

    /// <summary>
    /// 修改交易操作记录
    /// </summary>
    public void RecordChangeTrade(TradeSign sign)
    {
        List<string> param = new List<string>();
        param.Add(sign.tradeData.ID.ToString());
        param.Add(sign.tradeData.selectCashFlow.ToString());
        StageGoal.My.RecordOperation(OperationType.ChangeTrade,param);
    }

    /// <summary>
    /// 保存交易信息
    /// </summary>
    public void SaveTradeData()
    {
        currentTrade.tradeData.selectCashFlow = selectCashFlow;
        currentTrade.tradeData.castRole = castRole;
        currentTrade.tradeData.targetRole = targetRole;
    }

    /// <summary>
    /// 为交易生成图标
    /// </summary>
    public GameObject CreateTradeIcon(Transform start,Transform end)
    {
        //print("dasdasdsad");
        Vector3 pos = new Vector3(start.position.x * 0.5f + end.position.x * 0.5f, start.position.y * 0.5f + end.position.y * 0.5f + 2f, start.position.z * 0.5f + end.position.z * 0.5f);
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeIcon"));
        go.transform.position = pos;
        go.transform.SetParent(TradeManager.My.transform);
        //go.GetComponent<TradeIcon>().SetTradeIcon(selectSZFS,selectCashFlow,isFree,currentTrade.tradeData,currentTrade.tradeData.ID);
        return go;
    }

    /// <summary>
    /// 删除交易
    /// </summary>
    public void DeleteThisTrade()
    {
        NewCanvasUI.My.Panel_Delete.SetActive(true);
        gameObject.SetActive(false);
        string str = "确定要删除此交易吗？";
        DeleteUIManager.My.Init(str, () => { TradeManager.My.DeleteTrade(currentTrade.tradeData.ID); });
    }

    /// <summary>
    /// 清空交易配置面板
    /// </summary>
    public void DeleteTradeMenu()
    {
        startRolePanel.transform.localPosition = new Vector3(0f, startRolePanel.transform.localPosition.y, startRolePanel.transform.localPosition.z);
        endRolePanel.transform.localPosition = new Vector3(0f, endRolePanel.transform.localPosition.y, endRolePanel.transform.localPosition.z);
        Close();
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void Close()
    {
        
        //transform.localPosition = new Vector3(0f, 1000f, 0f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示窗口
    /// </summary>
    public void Show()
    {
        //transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    #region 计算交易成本

    public void CalculateTCOfTwo(string startRole,string endRole)
    {
        //todo
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Close();
    }
}
