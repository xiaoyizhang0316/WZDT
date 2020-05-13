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

    public void Init(string start, string end)
    {
        tradeData = new TradeData();
        tradeData.startRole = start;
        tradeData.endRole = end;
        tradeData.selectTradeDestination = TradeDestinationType.Warehouse;
        tradeData.isFree = false;
        isFirstSelect = true;
        tradeData.payRole = end;
        tradeData.receiveRole = start;
        tradeData.castRole = start;
        tradeData.targetRole = end;
        tradeData.ID = TradeManager.My.index++;
        tradeData.selectProduct = ProductType.Seed;
        tradeData.payPer = 0f;
        //print(TradeManager.My.index);
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
    /// 交易完成时
    /// </summary>
    public void Complete()
    {
        InvokeRepeating("ConductTrade",0f,1f);
    }

    /// <summary>
    /// 执行交易
    /// </summary>
    public void ConductTrade()
    {
        BaseMapRole role = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        RoleType start = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole)).baseRoleData.baseRoleData.roleType;
        RoleType end = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).baseRoleData.baseRoleData.roleType;
        SkillData data = GameDataMgr.My.GetSkillDataByName(tradeData.selectJYFS);
        if (data.skillLastType == SkillLastingType.Once)
        {
            if (role.ReleaseSkill(tradeData, ()=> {
                PayForTradeLast();
                Complete();
            }))
            {
                //print("交易执行");
                role.GetContribution(data.skillContribution);
                float result = CalculateTC();
                RecordHistory(result);
                PayForTrade();
                CancelInvoke();

            }
        }
        else if (data.skillLastType == SkillLastingType.Lasting)
        {
            CancelInvoke();
            InvokeRepeating("ConductLastingTrade",0f,60f);
            InvokeRepeating("PayForTradeLast", 59f, 60f);
        }
        //print(tradeData.selectJYFS);
    }

    /// <summary>
    /// 持续施法
    /// </summary>
    public void ConductLastingTrade()
    {
        BaseMapRole role = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        SkillData data = GameDataMgr.My.GetSkillDataByName(tradeData.selectJYFS);
        role.GetContribution(data.skillContribution);
        role.ReleaseSkill(tradeData);
        float result = CalculateTC();
        RecordHistory(result);
        PayForTrade();
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
