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
    /// 选择的分成比例
    /// </summary>
    public int selectDividePercent;

    private float startPer;

    private float endPer;

    public int startRoleTradeCost;

    public int endRoleTradeCost;

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

    public Slider divideSlider;

    public List<Image> startStatus;

    public List<Image> endStatus;

    public List<Sprite> encourageStatus;

    private bool startIsCast;

    /// <summary>
    /// 打开并初始化
    /// </summary>
    public void Open(GameObject tradeGo)
    {
        Show();
        currentTrade = tradeGo.GetComponent<TradeSign>();
        currentTrade.CheckClickTime();
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
        selectDividePercent = currentTrade.tradeData.dividePercent;
        startPer = currentTrade.startPer;
        endPer = currentTrade.endPer;
        castRole = currentTrade.tradeData.castRole;
        targetRole = currentTrade.tradeData.targetRole;
        startIsCast = currentTrade.tradeData.castRole.Equals(currentTrade.tradeData.startRole);
        startRoleTradeCost = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.startRole)).baseRoleData.tradeCost;
        endRoleTradeCost = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.endRole)).baseRoleData.tradeCost;
        InitName();
        InitRoleInfo();
        InitCashFlow();
        InitDivide();
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
        endRolePanel.Find("EndRoleTradeCost").GetComponent<Text>().text = endRoleTradeCost.ToString();
        endRolePanel.Find("EndRoleRisk").GetComponent<Text>().text = end.baseRoleData.riskResistance.ToString();
        startRolePanel.Find("StartRoleTradeCost").GetComponent<Text>().text = startRoleTradeCost.ToString();
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
        int result = (int)((startRoleTradeCost * startPer + startRole.baseRoleData.riskResistance));
        result += (int)((endRoleTradeCost * endPer + endRole.baseRoleData.riskResistance) );
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

    public void InitDivide()
    {
        divideSlider.value = selectDividePercent;
        for (int i = 0; i < startStatus.Count; i++)
        {
            startStatus[i].gameObject.SetActive(startIsCast && selectDividePercent > i);
        }
        for (int i = 0; i < endStatus.Count; i++)
        {
            endStatus[i].gameObject.SetActive(!startIsCast && Mathf.Abs(selectDividePercent - 4) > i);
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

    public void OnDivideValueChange()
    {
        selectDividePercent = (int)divideSlider.value;
        switch (selectDividePercent)
        {
            case 0:
                startPer = startIsCast?1f:0.6f;
                endPer = startIsCast?1f:1.4f;
                break;
            case 1:
                startPer = startIsCast?1.1f:0.7f;
                endPer = startIsCast?0.9f:1.3f;
                break;
            case 2:
                startPer = startIsCast?1.2f:0.8f;
                endPer = startIsCast?0.8f:1.2f;
                break;
            case 3:
                startPer = startIsCast?1.3f:0.9f;
                endPer = startIsCast?0.7f:1.1f;
                break;
            case 4:
                startPer = startIsCast?1.4f:1f;
                endPer = startIsCast?0.6f:1f;
                break;
            default:
                startPer = 1f;
                endPer = 1f;
                break;
        }
        for (int i = 0; i < startStatus.Count; i++)
        {
            startStatus[i].gameObject.SetActive(startIsCast && selectDividePercent > i);
        }
        for (int i = 0; i < endStatus.Count; i++)
        {
            endStatus[i].gameObject.SetActive(!startIsCast && Mathf.Abs(selectDividePercent - 4) > i);
        }
        PredictTradeCostChange();
        if (selectCashFlow == currentTrade.tradeData.selectCashFlow)
        {
            InitTradeCost();
        }
        else
        {
            tradeCostText.text = "???";
        }
    }

    public void PredictTradeCostChange()
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.endRole));
        if (startIsCast)
        {
            startRoleTradeCost = start.baseRoleData.tradeCost + (selectDividePercent - currentTrade.tradeData.dividePercent) * 5;
            startRolePanel.Find("StartRoleTradeCost").GetComponent<Text>().text = startRoleTradeCost.ToString();
        }
        else
        {
            endRoleTradeCost = end.baseRoleData.tradeCost + (currentTrade.tradeData.dividePercent - selectDividePercent) * 5;
            endRolePanel.Find("EndRoleTradeCost").GetComponent<Text>().text = endRoleTradeCost.ToString();
        }
    }

    /// <summary>
    /// 保存并退出
    /// </summary>
    public void SaveAndQuit()
    {
        bool isChange = (selectCashFlow != currentTrade.tradeData.selectCashFlow) || (selectDividePercent == currentTrade.tradeData.dividePercent);
        SaveTradeData();
        DeleteTradeMenu();
        if (isChange)
        {
            RecordChangeTrade(currentTrade);
            DataUploadManager.My.AddData(DataEnum.交易_改交易);
        }
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
        currentTrade.tradeData.dividePercent = selectDividePercent;
        currentTrade.startPer = startPer;
        currentTrade.endPer = endPer;
        currentTrade.UpdateEncourageLevel();
        if (!PlayerData.My.isSOLO)
        {
                string str1 = "ChangeTrade|";
                str1 += currentTrade.tradeData.ID.ToString();
                str1 += "," + selectCashFlow.ToString();
                str1 += "," + selectDividePercent;
                str1 += "," + startPer;
                str1 += "," + endPer;
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str1);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str1);
            }
        }
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
        DeleteUIManager.My.Init(str, () => {
            TradeManager.My.DeleteTrade(currentTrade.tradeData.ID);
            if (!PlayerData.My.isSOLO)
            {
                string str1 = "DeleteTrade|";
                str1 += currentTrade.tradeData.ID.ToString();
                if (PlayerData.My.isServer)
                {
                    PlayerData.My.server.SendToClientMsg(str1);
                }
                else
                {
                    PlayerData.My.client.SendToServerMsg(str1);
                }
            }
            DataUploadManager.My.AddData(DataEnum.交易_删交易);
        });
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
