using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class TradeSign : MonoBehaviour
{
    /// <summary>
    /// 数据类
    /// </summary>
    public TradeData tradeData;

    /// <summary>
    /// 是否是第一次修改
    /// </summary>
    public bool isFirstSelect;

    /// <summary>
    /// 交易图标Gameobject
    /// </summary>
    public GameObject tradeIconGo;

    /// <summary>
    /// 物流交易线Gameobject
    /// </summary>
    public GameObject tradeLineGo;

    /// <summary>
    /// 钱流交易线Gameobject
    /// </summary>
    public GameObject tradeMoneyLineGo;

    /// <summary>
    /// 单根管子
    /// </summary>
    public GameObject tradeCylinder;

    /// <summary>
    /// 信息流线
    /// </summary>
    public GameObject tradeBuffLine;

    public void Init(string start, string end)
    {
        tradeData = new TradeData();
        tradeData.startRole = start;
        tradeData.endRole = end;
        //tradeData.selectTradeDestination = TradeDestinationType.Warehouse;
        tradeData.isFree = false;
        isFirstSelect = true;
        tradeData.payRole = end;
        tradeData.receiveRole = start;
        tradeData.castRole = start;
        tradeData.targetRole = end;
        tradeData.selectSZFS = SZFSType.固定;
        tradeData.selectCashFlow = CashFlowType.先钱;
        tradeData.ID = TradeManager.My.index++;
        //tradeData.selectProduct = ProductType.Seed;
        //tradeData.payPer = 0f;
        //print(TradeManager.My.index);
        SetSkillTarget();
        AddTradeToRole();
        GenerateTradeLine();
    }

    /// <summary>
    /// 设置技能施法者
    /// </summary>
    public void SetSkillTarget()
    {
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        if (startRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
        {
            if (endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
            {
                tradeData.castRole = tradeData.startRole;
                tradeData.targetRole = tradeData.endRole;
            }
            else if (endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
            {
                tradeData.castRole = tradeData.endRole;
                tradeData.targetRole = tradeData.startRole;
            }
        }
        else if (startRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            if (endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
            {
                tradeData.castRole = tradeData.startRole;
                tradeData.targetRole = tradeData.endRole;
            }
        }
    }

    /// <summary>
    /// 为施法方添加技能
    /// </summary>
    public void AddTradeToRole()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        cast.tradeList.Add(this);
    }

    /// <summary>
    /// 生成交易线
    /// </summary>
    public void GenerateTradeLine()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
        {
            GenerateProductLine();
        }
        else
        {
            GenerateMoneyLine();
        }
    }

    /// <summary>
    /// 生成交易物流线
    /// </summary>
    public void GenerateProductLine()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        int number = Mathf.FloorToInt( Vector3.Distance(cast.transform.position, target.transform.position));
        Vector3 offset = (target.transform.position - cast.transform.position) / number;
        float HalfLength = Vector3.Distance(target.transform.position, cast.transform.position) / number / 2f;
        Vector3 tempStart = cast.transform.position;
        for (int i = 0; i < number; i++)
        {
            GameObject go = Instantiate(tradeCylinder);
            go.transform.SetParent(transform);
            Vector3 rightRotation = target.transform.position - cast.transform.position;
            float LThickness = 0.1f;
            go.transform.position = tempStart + offset;
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
            go.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
            tempStart = go.transform.position;
        }
        //lineGo = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //Vector3 rightPosition = (startTarget.gameObject.transform.position + Target) / 2;
        //
        //float HalfLength = Vector3.Distance(startTarget.transform.position, Target) / 2;
        //float LThickness = 0.1f;//线的粗细
        //创建圆柱体
        //lineGo.gameObject.transform.parent = transform;
        //lineGo.transform.position = rightPosition;
        //lineGo.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
        //lineGo.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
    }

    /// <summary>
    /// 生成交易信息流线
    /// </summary>
    public void GenerateMoneyLine()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        GameObject go = Instantiate(tradeBuffLine);
        go.transform.SetParent(transform);
        go.GetComponent<DrawMoneyLine>().InitPos(cast.transform, target.transform, tradeData.ID);
    }

    /// <summary>
    /// 清理所有线
    /// </summary>
    public void ClearAllLine()
    {
        Destroy(tradeLineGo, 0.001f);
        Destroy(tradeMoneyLineGo, 0.001f);
        Destroy(tradeIconGo, 0.001f);
    }
 
  

    /// <summary>
    /// 结算付钱关系(先钱)
    /// </summary>
    public void PayForTrade()
    {
        if (tradeData.isFree)
        {
            BaseMapRole payRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.payRole));
            payRole.RemovePayRelationShip(tradeData.ID);
            return;
        }
        else
        {
            if(tradeData.selectSZFS == SZFSType.固定 && tradeData.selectCashFlow == CashFlowType.先钱)
            {
                BaseMapRole castRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
                SkillData data = GameDataMgr.My.GetSkillDataByName(tradeData.selectJYFS);
                int payNum = (int)castRole.operationCost + data.cost;
                BaseMapRole payRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.payRole));
                payRole.PayToRole(tradeData.receiveRole, payNum);
                payRole.RemovePayRelationShip(tradeData.ID);
            }
            else if (tradeData.selectSZFS == SZFSType.分成)
            {
                BaseMapRole payRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.payRole));
                payRole.GetPayRelationShip(tradeData);
            }
        }
    }

    /// <summary>
    /// 结算固定后钱的关系
    /// </summary>
    public void PayForTradeLast()
    {
        //print("触发后钱回调");
        //print(tradeData.ID);
        if (tradeData.isFree)
        {
            return;
        }
        else
        {
            if (tradeData.selectSZFS == SZFSType.固定 && tradeData.selectCashFlow == CashFlowType.后钱)
            {
                BaseMapRole castRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
                SkillData data = GameDataMgr.My.GetSkillDataByName(tradeData.selectJYFS);
                int payNum = (int)castRole.operationCost + data.cost;
                BaseMapRole payRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.payRole));
                payRole.PayToRole(tradeData.receiveRole, payNum);
            }
        }
    }

    /// <summary>
    /// 生成交易历史记录
    /// </summary>
    public void RecordHistory(float num)
    {
        PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole)).GenerateTradeHistory(tradeData, num);
        PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)).GenerateTradeHistory(tradeData, num);
    }

    /// <summary>
    /// 结算交易成本
    /// </summary>
    public float CalculateTC()
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(tradeData.selectJYFS);
        if (data.supportThird)
        {
            float result1 = CalculateTCOfTwo(tradeData.startRole, tradeData.endRole);
            float result2 = CalculateTCOfTwo(tradeData.castRole, tradeData.thirdPartyRole);
            result1 = (result1 + result2) / 2f;
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole)).GetMoney(0 - (int)result1);
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole)).tradeCost += result1;
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).GetMoney(0 - (int)result1);
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).tradeCost += result1;
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.thirdPartyRole)).GetMoney(0 - (int)result1);
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.thirdPartyRole)).tradeCost += result1;
            return result1;
        }
        else
        {
            float result = CalculateTCOfTwo(tradeData.startRole, tradeData.endRole);
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole)).GetMoney(0 - (int)result);
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole)).tradeCost += result;
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).GetMoney(0 - (int)result);
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).tradeCost += result;
            return result;
        }
    }

    /// <summary>
    /// 结算交易成本
    /// </summary>
    public float CalculateTCOfTwo(string startRole,string endRole)
    {
        //todo
       //Role start = PlayerData.My.GetRoleById(double.Parse(startRole));
       //Role end = PlayerData.My.GetRoleById(double.Parse(endRole));
       //Role conduct = PlayerData.My.GetRoleById(double.Parse(tradeData.castRole));
       ////print(start.fixedCost);
       ////print(end.fixedCost);
       ////print(PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole)).AllSkills.Count);
       ////print(PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).AllSkills.Count);
       ////if (start == null || end == null || PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole)) == null || PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)) == null)
       ////    return; 
       //TradeSkillData data = GameDataMgr.My.GetSkillDataByStartEndConductSkill(start.baseRoleData.roleType, end.baseRoleData.roleType, conduct.baseRoleData.roleType, tradeData.selectJYFS);
       //float resultSearch = 0f;
       //float resultBargain = 0f;
       //float resultDeliver = 0f;
       //float risk = 0f;
       //List<float> resultList;
       ////外部
       //if (PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole)).isNpc || PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).isNpc)
       //{
       //    resultSearch += data.searchOutPerA * start.search + data.searchOutPerB * end.search;
       //    resultBargain += data.bargainOutPerA * start.bargain + data.bargainOutPerB * end.bargain;
       //    if (data.conductRole == start.baseRoleData.roleType)
       //    {
       //        resultDeliver += data.deliverOutPerA * start.delivery + data.deliverOutPerB * end.delivery;
       //    }
       //    else
       //    {
       //        resultDeliver += data.deliverOutPerA * end.delivery + data.deliverOutPerB * start.delivery;
       //    }
       //    risk = data.riskOutPerA * start.risk + data.riskOutPerB * end.risk;
       //    resultList = GetDealConfigAdd(false);
       //}
       ////内部
       //else
       //{
       //    resultSearch += (data.searchInPerA * start.search + data.searchInPerB * end.search) * data.searchInAdd;
       //    resultBargain += (data.bargainInPerA * start.bargain + data.bargainInPerB * end.bargain) * data.bargainInAdd;
       //    if (data.conductRole == start.baseRoleData.roleType)
       //    {
       //        resultDeliver += (data.deliverInPerA * start.delivery + data.deliverInPerB * end.delivery) * data.deliverInAdd;
       //    }
       //    else
       //    {
       //        resultDeliver += (data.deliverInPerA * end.delivery + data.deliverInPerB * start.delivery) * data.deliverInAdd;
       //    }
       //    risk = data.riskInPerA * start.risk + data.riskInPerB * end.risk;
       //    resultList = GetDealConfigAdd(true);
       //}
       // float totalCost = resultSearch * resultList[0] + resultBargain * resultList[1] + resultDeliver * resultList[2];
       // totalCost = totalCost * 8;
       // //print("扣除交易成本，数量为：" + totalCost);
         return 0;
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
        if (tradeData.isFree)
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
        else if (tradeData.selectSZFS == SZFSType.固定)
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
            if (tradeData.selectCashFlow == CashFlowType.先钱)
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
            else if (tradeData.selectCashFlow == CashFlowType.后钱)
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
        else if (tradeData.selectSZFS == SZFSType.剩余)
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
            if (tradeData.selectCashFlow == CashFlowType.先钱)
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
            else if (tradeData.selectCashFlow == CashFlowType.后钱)
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
        else if (tradeData.selectSZFS == SZFSType.分成)
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
            if (tradeData.selectCashFlow == CashFlowType.先钱)
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
            else if (tradeData.selectCashFlow == CashFlowType.后钱)
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
}
