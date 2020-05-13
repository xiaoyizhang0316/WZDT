using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;

public class TradeManager : MonoSingleton<TradeManager>
{
    public int index;

    /// <summary>
    /// 所有交易的列表
    /// </summary>
    public Dictionary<int, TradeSign> tradeList;

    /// <summary>
    /// 所有的交易图标列表
    /// </summary>
    public List<TradeIcon> tradeIcons;

    /// <summary>
    /// 所有的交易线列表
    /// </summary>
    public List<DrawLine> tradeLines;

    /// <summary>
    /// 创建指定对象指定技能的交易（用于AI自动创建）
    /// </summary>
    public void CreateTradeAI(double startID,double endId,string skillName,ProductType productType,TradeDestinationType destinationType)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeSign"));
        go.transform.SetParent(transform);
        TradeSign sign = go.GetComponent<TradeSign>();
        sign.Init(startID.ToString(), endId.ToString());
        //print(sign.tradeData.ID);
        TradeSettingAI(ref sign, skillName, productType, destinationType);
        DrawTradeLineAI(ref sign);
        tradeList.Add(sign.tradeData.ID, sign);
        sign.isFirstSelect = false;
       
    }

    /// <summary>
    /// 交易设置
    /// </summary>
    /// <param name="tradeSign"></param>
    /// <param name="skillName"></param>
    public void TradeSettingAI(ref TradeSign tradeSign,string skillName, ProductType productType, TradeDestinationType destinationType)
    {
        tradeSign.tradeData.payRole = tradeSign.tradeData.endRole;
        tradeSign.tradeData.receiveRole = tradeSign.tradeData.startRole;
        tradeSign.tradeData.selectJYFS = skillName;
        SkillData data = GameDataMgr.My.GetSkillDataByName(skillName);
        tradeSign.tradeData.selectCashFlow = data.supportCashFlow[0];
        tradeSign.tradeData.selectSZFS = data.supportSZFS[0];
        tradeSign.tradeData.selectProduct = productType;
        tradeSign.tradeData.selectTradeDestination = destinationType;
    }

    public void DrawTradeLineAI(ref TradeSign sign)
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.endRole));
        BaseMapRole pay = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.payRole));
        BaseMapRole receive = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.receiveRole));
        sign.tradeLineGo = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeLine"), transform.position, transform.rotation, transform);
        sign.tradeLineGo.GetComponent<DrawLine>().InitPos(start.transform, end.transform, sign.tradeData.ID);
        sign.tradeMoneyLineGo = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeMoneyLine"), transform.position, transform.rotation, transform);
        sign.tradeMoneyLineGo.GetComponent<DrawMoneyLine>().InitPos(pay.transform, receive.transform, sign.tradeData.ID);
        Vector3 pos = new Vector3(start.transform.position.x * 0.5f + end.transform.position.x * 0.5f, start.transform.position.y * 0.5f +
            end.transform.position.y * 0.5f + 2f, start.transform.position.z * 0.5f + end.transform.position.z * 0.5f);
        if (start.isNpc && end.isNpc)
        {
            return;
        }
        sign.tradeIconGo = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeIcon"));
        sign.tradeIconGo.transform.position = pos;
        sign.tradeIconGo.transform.SetParent(transform);
        sign.tradeIconGo.GetComponent<TradeIcon>().SetTrasform(start.transform,end.transform);
        sign.tradeIconGo.GetComponent<TradeIcon>().SetTradeIcon(sign.tradeData.selectSZFS, sign.tradeData.selectCashFlow, sign.tradeData.isFree, sign.tradeData, sign.tradeData.ID);
    }

    /// <summary>
    /// 删除指定ID的交易
    /// </summary>
    /// <param name="ID"></param>
    public void DeleteTrade(int ID)
    {
        if (tradeList.ContainsKey(ID))
        {
            TradeSign temp = tradeList[ID];
            tradeList.Remove(ID);
            temp.ClearAllLine();
            BaseMapRole payRole = PlayerData.My.GetMapRoleById(double.Parse(temp.tradeData.payRole));
            payRole.RemovePayRelationShip(ID);
            Destroy(temp.gameObject, 0f);
            CreateTradeManager.My.Close();
        }
    }

    /// <summary>
    /// 删除某个角色相关的所有交易
    /// </summary>
    /// <param name="roleID"></param>
    public void DeleteRoleAllTrade(double roleID)
    {
        if (tradeList.Count == 0)
            return;
        List<int> temp = new List<int>(tradeList.Keys);
        for (int i = 0; i < temp.Count; i++)
        {
            if (Mathf.Abs((float)(double.Parse(tradeList[temp[i]].tradeData.startRole) - roleID)) < 0.1f || Mathf.Abs((float)(double.Parse(tradeList[temp[i]].tradeData.endRole) - roleID)) < 0.1f)
            {
                DeleteTrade(temp[i]);
            }
        }
    }

   

    private void OnDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        tradeList = new Dictionary<int, TradeSign>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
