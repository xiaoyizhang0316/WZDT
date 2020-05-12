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
    /// 选择的目的地
    /// </summary>
    public TradeDestinationType selectTradeDestination;

    /// <summary>
    /// 选择的产品
    /// </summary>
    public ProductType selectProduct;

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
    /// 分成比例
    /// </summary>
    public float payPerc;

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

    public List<TradeSkillData> availableTradeSkill;

    public TradeRoleAttribute castRoleAttribute;

    public TradeRoleAttribute targetRoleAttribute;

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
        selectTradeDestination = currentTrade.tradeData.selectTradeDestination;
        selectCashFlow = currentTrade.tradeData.selectCashFlow;
        isFree = currentTrade.tradeData.isFree;
        payRole = currentTrade.tradeData.payRole;
        receiveRole = currentTrade.tradeData.receiveRole;
        castRole = currentTrade.tradeData.castRole;
        targetRole = currentTrade.tradeData.targetRole;
        selectProduct = currentTrade.tradeData.selectProduct;
        thirdPartyRole = currentTrade.tradeData.thirdPartyRole;
        payPerc = currentTrade.tradeData.payPer;
        availableTradeSkill = new List<TradeSkillData>();
        InitName();
        JYFS.My.Init();
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
        if (ExecutionManager.My.SubExecution(ExecutionManager.My.modifyDeal))
        {
            currentTrade.isFirstSelect = false;
            if (TradeManager.My.tradeList.ContainsKey(currentTrade.tradeData.ID))
            {
                ChangeMoneyLine();
                SaveTradeData();
                TradeManager.My.tradeList[currentTrade.tradeData.ID] = currentTrade;
                Transform start = CommonData.My.RoleTF.transform.Find(currentTrade.tradeData.startRole);
                Transform end = CommonData.My.RoleTF.transform.Find(currentTrade.tradeData.endRole);
                TradeManager.My.tradeList[currentTrade.tradeData.ID].tradeIconGo.GetComponent<TradeIcon>().SetTradeIcon(selectSZFS, selectCashFlow, isFree, currentTrade.tradeData, currentTrade.tradeData.ID);
            }
            else
            {
                SaveTradeData();
                TradeManager.My.tradeList.Add(currentTrade.tradeData.ID, currentTrade);
                Transform start = CommonData.My.RoleTF.transform.Find(currentTrade.tradeData.startRole);
                Transform end = CommonData.My.RoleTF.transform.Find(currentTrade.tradeData.endRole);
                TradeManager.My.tradeList[currentTrade.tradeData.ID].tradeLineGo = CreateTradeLine(start, end);
                //if (!isFree)
                {
                    Transform moneyStart = CommonData.My.RoleTF.transform.Find(currentTrade.tradeData.payRole);
                    Transform moneyEnd = CommonData.My.RoleTF.transform.Find(currentTrade.tradeData.receiveRole);
                    TradeManager.My.tradeList[currentTrade.tradeData.ID].tradeMoneyLineGo = CreateTradeMoneyLine(moneyStart, moneyEnd);
                }
                TradeManager.My.tradeList[currentTrade.tradeData.ID].tradeIconGo = CreateTradeIcon(start, end);
                TradeManager.My.tradeList[currentTrade.tradeData.ID].tradeIconGo.GetComponent<TradeIcon>().SetTrasform(start, end);
                print(TradeManager.My.tradeList[currentTrade.tradeData.ID].tradeData.selectProduct);
                TradeManager.My.tradeList[currentTrade.tradeData.ID].Complete();
            }
            DeleteTradeMenu();
        }
    }

    /// <summary>
    /// 保存交易信息
    /// </summary>
    public void SaveTradeData()
    {
        //print(selectProduct);
        currentTrade.tradeData.selectSZFS = selectSZFS;
        currentTrade.tradeData.selectCashFlow = selectCashFlow;
        currentTrade.tradeData.selectTradeDestination = selectTradeDestination;
        currentTrade.tradeData.selectJYFS = selectJYFS;
        currentTrade.tradeData.isFree = isFree;
        currentTrade.tradeData.payRole = payRole;
        currentTrade.tradeData.receiveRole = receiveRole;
        currentTrade.tradeData.castRole = castRole;
        currentTrade.tradeData.targetRole = targetRole;
        currentTrade.tradeData.selectProduct = selectProduct;
        currentTrade.tradeData.thirdPartyRole = thirdPartyRole;
        currentTrade.tradeData.payPer = payPerc;
    }

    /// <summary>
    /// 判断钱流线是否需要增加/删除
    /// </summary>
    public void ChangeMoneyLine()
    {
        DrawMoneyLine[] temp = FindObjectsOfType<DrawMoneyLine>();
        //if (!isFree)
        {
            bool alreadyHas = false;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].ID == currentTrade.tradeData.ID)
                {
                    List<string> tempStrList = new List<string>();
                    tempStrList.Add(TradeManager.My.tradeList[temp[i].ID].tradeData.payRole);
                    tempStrList.Add(TradeManager.My.tradeList[temp[i].ID].tradeData.receiveRole);
                    if (tempStrList.Contains(payRole) && tempStrList.Contains(receiveRole))
                    {
                        //print("发起者承受者相同");
                        alreadyHas = true;
                    }
                    else
                    {
                        //print("发起者承受者不同");
                        Destroy(temp[i].gameObject, 0.01f);
                    }

                }
            }
            if (!alreadyHas)
            {
                Transform moneyStart = CommonData.My.RoleTF.transform.Find(payRole);
                Transform moneyEnd = CommonData.My.RoleTF.transform.Find(receiveRole);
                TradeManager.My.tradeList[currentTrade.tradeData.ID].tradeMoneyLineGo = CreateTradeMoneyLine(moneyStart, moneyEnd);
            }
        }
        //else
        //{
        //    for (int i = 0; i < temp.Length; i++)
        //    {
        //        if (temp[i].ID == currentTrade.tradeData.ID)
        //        {
        //            Destroy(temp[i].gameObject, 0.01f);
        //            break;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// 为交易生成物流线
    /// </summary>
    public GameObject CreateTradeLine(Transform start,Transform end)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeLine"), TradeManager.My.transform.position, TradeManager.My.transform.rotation, TradeManager.My.transform);
        go.GetComponent<DrawLine>().InitPos(start, end,currentTrade.tradeData.ID);
        return go;
    }

    /// <summary>
    /// 为交易生成钱流线
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public GameObject CreateTradeMoneyLine(Transform start,Transform end)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeMoneyLine"), TradeManager.My.transform.position, TradeManager.My.transform.rotation, TradeManager.My.transform);
        go.GetComponent<DrawMoneyLine>().InitPos(start, end, currentTrade.tradeData.ID);
        return go;
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

    /// <summary>
    /// 更新交易成本面板数字  
    /// </summary>
    public void UpdateTCInfo(float search,float bargain,float delvier,float risk)
    {
        Text[] texts = TCGO.GetComponentsInChildren<Text>();
        float total = search + bargain + delvier;
        texts[0].text = Mathf.Floor(search).ToString();
        texts[1].text = Mathf.Floor(bargain).ToString();
        texts[2].text = Mathf.Floor(delvier).ToString();
        texts[3].text = Mathf.Floor(risk).ToString();
        texts[4].text = Mathf.Floor(total).ToString();
    }

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

    /// <summary>
    /// 结算交易结构附加量
    /// </summary>
    /// <param name="isInside"></param>
    /// <returns></returns>
    public List<float> GetDealConfigAdd(bool isInside)
    {
        float searchAdd = 1f;
        float bargainAdd = 1f;
        float deliverAdd = 1f;
        float riskAdd = 1f;
        if (isFree)
        {
            if (isInside)
            {
                searchAdd += DealConfigData.My.free[0];
                bargainAdd += DealConfigData.My.free[1];
                deliverAdd += DealConfigData.My.free[2];
                riskAdd += DealConfigData.My.free[3];
            }
            else
            {
                searchAdd += DealConfigData.My.free[4];
                bargainAdd += DealConfigData.My.free[5];
                deliverAdd += DealConfigData.My.free[6];
                riskAdd += DealConfigData.My.free[7];
            }
        }
        else if (selectSZFS == SZFSType.固定)
        {
            if (isInside)
            {
                searchAdd += DealConfigData.My.fix[0];
                bargainAdd += DealConfigData.My.fix[1];
                deliverAdd += DealConfigData.My.fix[2];
                riskAdd += DealConfigData.My.fix[3];
            }
            else
            {
                searchAdd += DealConfigData.My.fix[4];
                bargainAdd += DealConfigData.My.fix[5];
                deliverAdd += DealConfigData.My.fix[6];
                riskAdd += DealConfigData.My.fix[7];
            }
            if (selectCashFlow == CashFlowType.先钱)
            {
                if (isInside)
                {
                    searchAdd += DealConfigData.My.moneyFirst[0];
                    bargainAdd += DealConfigData.My.moneyFirst[1];
                    deliverAdd += DealConfigData.My.moneyFirst[2];
                    riskAdd += DealConfigData.My.moneyFirst[3];
                }
                else
                {
                    searchAdd += DealConfigData.My.moneyFirst[4];
                    bargainAdd += DealConfigData.My.moneyFirst[5];
                    deliverAdd += DealConfigData.My.moneyFirst[6];
                    riskAdd += DealConfigData.My.moneyFirst[7];
                }
            }
            else if (selectCashFlow == CashFlowType.后钱)
            {
                if (isInside)
                {
                    searchAdd += DealConfigData.My.moneyLast[0];
                    bargainAdd += DealConfigData.My.moneyLast[1];
                    deliverAdd += DealConfigData.My.moneyLast[2];
                    riskAdd += DealConfigData.My.moneyLast[3];
                }
                else
                {
                    searchAdd += DealConfigData.My.moneyLast[4];
                    bargainAdd += DealConfigData.My.moneyLast[5];
                    deliverAdd += DealConfigData.My.moneyLast[6];
                    riskAdd += DealConfigData.My.moneyLast[7];
                }
            }
        }
        else if (selectSZFS == SZFSType.剩余)
        {
            if (isInside)
            {
                searchAdd += DealConfigData.My.rest[0];
                bargainAdd += DealConfigData.My.rest[1];
                deliverAdd += DealConfigData.My.rest[2];
                riskAdd += DealConfigData.My.rest[3];
            }
            else
            {
                searchAdd += DealConfigData.My.rest[4];
                bargainAdd += DealConfigData.My.rest[5];
                deliverAdd += DealConfigData.My.rest[6];
                riskAdd += DealConfigData.My.rest[7];
            }
            if (selectCashFlow == CashFlowType.先钱)
            {
                if (isInside)
                {
                    searchAdd += DealConfigData.My.moneyFirst[0];
                    bargainAdd += DealConfigData.My.moneyFirst[1];
                    deliverAdd += DealConfigData.My.moneyFirst[2];
                    riskAdd += DealConfigData.My.moneyFirst[3];
                }
                else
                {
                    searchAdd += DealConfigData.My.moneyFirst[4];
                    bargainAdd += DealConfigData.My.moneyFirst[5];
                    deliverAdd += DealConfigData.My.moneyFirst[6];
                    riskAdd += DealConfigData.My.moneyFirst[7];
                }
            }
            else if (selectCashFlow == CashFlowType.后钱)
            {
                if (isInside)
                {
                    searchAdd += DealConfigData.My.moneyLast[0];
                    bargainAdd += DealConfigData.My.moneyLast[1];
                    deliverAdd += DealConfigData.My.moneyLast[2];
                    riskAdd += DealConfigData.My.moneyLast[3];
                }
                else
                {
                    searchAdd += DealConfigData.My.moneyLast[4];
                    bargainAdd += DealConfigData.My.moneyLast[5];
                    deliverAdd += DealConfigData.My.moneyLast[6];
                    riskAdd += DealConfigData.My.moneyLast[7];
                }
            }
        }
        else if (selectSZFS == SZFSType.分成)
        {
            if (isInside)
            {
                searchAdd += DealConfigData.My.divide[0];
                bargainAdd += DealConfigData.My.divide[1];
                deliverAdd += DealConfigData.My.divide[2];
                riskAdd += DealConfigData.My.divide[3];
            }
            else
            {
                searchAdd += DealConfigData.My.divide[4];
                bargainAdd += DealConfigData.My.divide[5];
                deliverAdd += DealConfigData.My.divide[6];
                riskAdd += DealConfigData.My.divide[7];
            }
            if (selectCashFlow == CashFlowType.先钱)
            {
                if (isInside)
                {
                    searchAdd += DealConfigData.My.moneyFirst[0];
                    bargainAdd += DealConfigData.My.moneyFirst[1];
                    deliverAdd += DealConfigData.My.moneyFirst[2];
                    riskAdd += DealConfigData.My.moneyFirst[3];
                }
                else
                {
                    searchAdd += DealConfigData.My.moneyFirst[4];
                    bargainAdd += DealConfigData.My.moneyFirst[5];
                    deliverAdd += DealConfigData.My.moneyFirst[6];
                    riskAdd += DealConfigData.My.moneyFirst[7];
                }
            }
            else if (selectCashFlow == CashFlowType.后钱)
            {
                if (isInside)
                {
                    searchAdd += DealConfigData.My.moneyLast[0];
                    bargainAdd += DealConfigData.My.moneyLast[1];
                    deliverAdd += DealConfigData.My.moneyLast[2];
                    riskAdd += DealConfigData.My.moneyLast[3];
                }
                else
                {
                    searchAdd += DealConfigData.My.moneyLast[4];
                    bargainAdd += DealConfigData.My.moneyLast[5];
                    deliverAdd += DealConfigData.My.moneyLast[6];
                    riskAdd += DealConfigData.My.moneyLast[7];
                }
            }
        }
        List<float> result = new List<float>();
        result.Add(searchAdd);
        result.Add(bargainAdd);
        result.Add(deliverAdd);
        result.Add(riskAdd);
        return result;
    }

    #endregion

    public void PopUpShow(string str,Vector3 pos)
    {
        popUpPanel.SetActive(true);
        popUpPanel.GetComponent<TradeSettingPopUp>().Init(str,pos);
    }

    public void PopUpHide()
    {
        popUpPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        PopUpHide();
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
    [Serializable]
    public class TradeRoleAttribute
    {
        public int brand;

        public int quality;

        public int capacity;

        public int effeciency;

        public int search;

        public int bargain;

        public int delivery;

        public int risk;
    }
}
