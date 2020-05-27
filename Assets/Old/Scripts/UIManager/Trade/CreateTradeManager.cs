using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;
using UnityEngine.UI;
using System;

public class CreateTradeManager : MonoSingleton<CreateTradeManager>
{
    /// <summary>
    /// 当前的交易
    /// </summary>
    public TradeSign currentTrade;

    /// <summary>
    /// 选择的交易方式
    /// </summary>
    public string selectJYFS;

    /// <summary>
    /// 选择的收支方式
    /// </summary>
    public SZFSType selectSZFS;

    /// <summary>
    /// 选择的现金流结构
    /// </summary>
    public CashFlowType selectCashFlow;

    /// <summary>
    /// 是否免费
    /// </summary>
    public bool isFree;

    /// <summary>
    /// 付钱方
    /// </summary>
    public string payRole;

    /// <summary>
    /// 收钱方
    /// </summary>
    public string receiveRole;

    /// <summary>
    /// 释放技能者
    /// </summary>
    public string castRole;

    /// <summary>
    /// 技能目标
    /// </summary>
    public string targetRole;

    /// <summary>
    /// 技能第三方目标
    /// </summary>
    public string thirdPartyRole;

    /// <summary>
    /// 技能是否满足释放条件
    /// </summary>
    public bool isSkillCanRelease;


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
    /// 交易成本面板gameobject
    /// </summary>
    public GameObject TCGO;

    /// <summary>
    /// 悬浮提示面板
    /// </summary>
    public GameObject popUpPanel;

    /// <summary>
    /// 发起者信息
    /// </summary>
    public GameObject startRoleInfo;

    /// <summary>
    /// 承受者信息
    /// </summary>
    public GameObject endRoleInfo;

    /// <summary>
    /// 打开并初始化
    /// </summary>
    public void Open(GameObject tradeGo)
    {
        Show();
        currentTrade = tradeGo.GetComponent<TradeSign>() ;
        Init();
    }

    /// <summary>
    /// 初始化 
    /// </summary>
    public void Init()
    {
        selectSZFS = currentTrade.tradeData.selectSZFS;
        selectCashFlow = currentTrade.tradeData.selectCashFlow;
        isFree = currentTrade.tradeData.isFree;
        castRole = currentTrade.tradeData.castRole;
        targetRole = currentTrade.tradeData.targetRole;
        InitName();
    }

    /// <summary>
    /// 初始化发起者和承受者名字
    /// </summary>
    public void InitName()
    {
        startName.text = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.startRole)).baseRoleData.baseRoleData.roleName;
        endName.text = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.endRole)).baseRoleData.baseRoleData.roleName;
        string startType = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.startRole)).baseRoleData.baseRoleData.roleType.ToString();
        string endType = PlayerData.My.GetMapRoleById(double.Parse(currentTrade.tradeData.endRole)).baseRoleData.baseRoleData.roleType.ToString();
        startLogo.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + startType);
        endLogo.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + endType);

    }

    /// <summary>
    /// 保存并退出
    /// </summary>
    public void SaveAndQuit()
    {
        SaveTradeData();
        DeleteTradeMenu();
    }

    /// <summary>
    /// 保存交易信息
    /// </summary>
    public void SaveTradeData()
    {
        currentTrade.tradeData.selectSZFS = selectSZFS;
        currentTrade.tradeData.selectCashFlow = selectCashFlow;
        currentTrade.tradeData.isFree = isFree;
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
        go.GetComponent<TradeIcon>().SetTradeIcon(selectSZFS,selectCashFlow,isFree,currentTrade.tradeData,currentTrade.tradeData.ID);
        return go;
    }

    /// <summary>
    /// 删除交易
    /// </summary>
    public void DeleteThisTrade()
    {
        UIManager.My.Panel_Confirm.gameObject.SetActive(true);
        string str = "确定要删除此交易吗？";
        UIManager.My.Panel_Confirm.gameObject.GetComponent<ConfirmPanel>().Init(TradeManager.My.DeleteTrade, currentTrade.tradeData.ID, str);
    }

    /// <summary>
    /// 清空交易配置面板
    /// </summary>
    public void DeleteTradeMenu()
    {
        if (!TradeManager.My.tradeList.ContainsKey(currentTrade.tradeData.ID))
        {
            Destroy(currentTrade.gameObject, 0.1f);
        }
        Close();
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void Close()
    {
        transform.localPosition = new Vector3(0f, 1000f, 0f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示窗口
    /// </summary>
    public void Show()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }


    #region 计算交易成本

    public void CalculateTCOfTwo(string startRole,string endRole)
    {
        //todo
   //    Role start = PlayerData.My.GetRoleById(double.Parse(startRole));
   //    Role end = PlayerData.My.GetRoleById(double.Parse(endRole));
   //    Role conduct = PlayerData.My.GetRoleById(double.Parse(castRole));
   //    TradeSkillData data = GameDataMgr.My.GetSkillDataByStartEndConductSkill(start.baseRoleData.roleType, end.baseRoleData.roleType, conduct.baseRoleData.roleType, selectJYFS);
   //    float resultSearch = 0f;
   //    float resultBargain = 0f;
   //    float resultDeliver = 0f;
   //    float resultRisk = 0f;
   //    List<float> resultList;
   //    //外部
   //    if (PlayerData.My.GetMapRoleById(double.Parse(startRole)).isNpc || PlayerData.My.GetMapRoleById(double.Parse(endRole)).isNpc)
   //    {
   //        resultSearch += data.searchOutPerA * start.search + data.searchOutPerB * end.search;
   //        resultBargain += data.bargainOutPerA * start.bargain + data.bargainOutPerB * end.bargain;
   //        if (data.conductRole == start.baseRoleData.roleType)
   //        {
   //            resultDeliver += data.deliverOutPerA * start.delivery + data.deliverOutPerB * end.delivery;
   //        }
   //        else
   //        {
   //            resultDeliver += data.deliverOutPerA * end.delivery + data.deliverOutPerB * start.delivery;
   //        }
   //        resultRisk = data.riskOutPerA * start.risk + data.riskOutPerB * end.risk;
   //        resultList = GetDealConfigAdd(false);
   //    }
   //    //内部
   //    else
   //    {
   //        resultSearch += (data.searchInPerA * start.search + data.searchInPerB * end.search) * data.searchInAdd;
   //        resultBargain += (data.bargainInPerA * start.bargain + data.bargainInPerB * end.bargain) * data.bargainInAdd;
   //        if (data.conductRole == start.baseRoleData.roleType)
   //        {
   //            resultDeliver += (data.deliverInPerA * start.delivery + data.deliverInPerB * end.delivery) * data.deliverInAdd;
   //        }
   //        else
   //        {
   //            resultDeliver += (data.deliverInPerA * end.delivery + data.deliverInPerB * start.delivery) * data.deliverInAdd;
   //        }
   //        resultRisk = data.riskInPerA * start.risk + data.riskInPerB * end.risk;
   //        resultList = GetDealConfigAdd(true);
   //    }
   //    float search = resultSearch * resultList[0];
   //    float bargain = resultBargain * resultList[1];
   //    float deliver = resultDeliver * resultList[2];
   //    float risk = resultRisk * resultList[3];
   //    UpdateTCInfo(search, bargain, deliver, risk);
    }

   

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTrade != null)
        {
            transform.Find("Ensure").GetComponent<Button>().interactable = isSkillCanRelease;
        }
    }
}
